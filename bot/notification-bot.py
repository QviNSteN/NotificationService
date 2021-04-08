import os
import time
import json
import requests
import telebot
from flask import Flask, request, jsonify
from telebot import types

app = Flask(__name__)
TOKEN = ""
SSL_PUBLIC_KEY = '/etc/ssl/certs/bot-ssl.crt'
SSL_PRIVATE_KEY = '/etc/ssl/private/bot-ssl.key'
WEBHOOK_PORT = 8443
WEBHOOK_HOST = ''
WEBHOOK_LISTEN = '0.0.0.0'
WEBHOOK_URL_BASE = "https://%s:%s" % (WEBHOOK_HOST, WEBHOOK_PORT)
WEBHOOK_URL_PATH = "/%s/" % (TOKEN)

bot = telebot.TeleBot(TOKEN)
user_auth_url = ''
get_user_info_url = 'http://localhost/user/{}'
test_user_id = 0
identity_token = ''

def get_user_chatId_test(id):
    return "{}".format(test_user_id)
    
def integration_new_user_test(unique_code, chat_id):
    return 'Аляяляев Аляля Алялалялялалял'

def extract_unique_code_test(id):
    return "dasdasda"

def extract_unique_code(text):
    return text.split()[1] if len(text.split()) > 1 else None
    
def get_command(command):
    return command.split()[0] if len(command.split()) > 1 else command

def get_user_chatId(id):
    url = get_user_info_url.format(id)
    headers = {'content-type': 'application/json', 'Token': "{}".format(identity_token)}
    response = requests.get(url, headers=headers)
    data = response.json()
    if 'integrations' in data:
        chat_ids = [x['value'] for x in data['integrations'] if x['type'] == 'Telegram'] 
        return chat_ids if len(chat_ids) > 0 else None
    else:
        return None

def integration_new_user(unique_code, chat_id):
    params = {"code": "{}".format(unique_code), "integrationData": "{}".format(chat_id)}
    headers = {'content-type': 'application/json', 'Token': "{}".format(identity_token)}
    response = requests.post(user_auth_url, data = json.dumps(params), headers=headers)
    data = response.json()
    if 'simpleFio' in data:
        fio = data['simpleFio']
        return fio if fio else None
    elif 'errorMessage' in data:
    return None

def bot_send_message(chat_id, message):
    if chat_id:
        bot.send_message(chat_id, message , parse_mode='Markdown', reply_markup=types.ReplyKeyboardRemove())

def text_notification(body):
    return '*Поступило новое уведомление:*\n*----------------------------------------*\n{0}'.format(body)

def error_text():
    return "Бот доступен только при авторизации!\nПожалуйста, авторизируйтесь в нашей системе, перейдите во вкладку \'Интеграции\', выберите пункт \'Telegram\' и нажмите \'Подключить\'!\n\nОбещаю, после данных действий, мы встретимся вновь!"

def files_in_text(files):
    files_text = ', '.join([f"[{i['filename']}]({i['link']})" for i in files ])
    if len(files) == 1:
        return "\n_Приложенный файл:_ " + files_text + "_._"
    else:
        return "\n_Приложенные файлы:_ " + files_text + "_._"

def startCommand(message):
    unique_code = extract_unique_code(message.text)
    if unique_code:
        username = integration_new_user(unique_code, message.chat.id)
        if username:
            reply = f'Здравствуйте, *{username}*!\nУведомления подключены!'
        else:
            reply = 'Прошу прощения, но вы ввели неверный токен!\n{}'.format(error_text())
    else:
        reply = error_text()
    bot_send_message(message.chat.id, reply)

def send_text(message):
    bot_send_message(message.chat.id,  'Прости, но я выполняю функцию исключительно рассылки уведомлений! Я крайне ограниченный бот и не запрограммирован на возможность поболтать :(')

@app.route('/', methods=['GET'])
def test_get():
    return 'TEST'

@app.route('/new-notification/' , methods=["POST"])
def new_notification():
    content = request.get_json()
    body = content['body']
    user_id = content['userId']
    files = content['files'] if 'files' in content else []
    if len(files) != 0:
        body = body + files_in_text(files)
    user_ids = get_user_chatId(user_id)
    if user_ids:
        for chat_id in user_ids:
            bot_send_message(int(chat_id), text_notification(body))
    return text_notification(body)

@app.route('/{}'.format(TOKEN), methods=["POST"])
def webhook():
    data = request.get_data().decode('utf-8')
    update = telebot.types.Update.de_json(data)
    
    if update.message.entities and len(update.message.entities) and update.message.entities[0].type == 'bot_command' and get_command(update.message.text) == '/start':
        startCommand(update.message)
    else:
        send_text(update.message)
    return ''
