from flask import Flask, render_template, request, redirect
from BackEnd.Controllers import userController
from BackEnd.Controllers import apostaColetivaController as aposta

app = Flask(__name__)


# rota a usar para receber os dados de um user em particular, definido pelo user_name
# devolve o resultado em json/dict
@app.route('/user/<user_name>')
def user(user_name):
    user = userController.getuser(user_name)
    if not user:
        return {}

    return {
        "user_id": user[0][0],
        "name": user[0][1],
        "password": user[0][2],
        "amount": user[0][3],
        "isAdmin": user[0][4],
        "email": user[0][5],
        "nif": user[0][6],
        "dn": user[0][7]
    }


# rota usada para registar um user. 
# os dados do User (name, password, amount, isAdmin) devem ser passados em formato json ou entao da erro
@app.route('/user/register', methods=['POST'])
def register():
    username = request.json['username']

    available = userController.checkuserexists(username)
    if (available == "0"):
        name = request.json['nome']
        password = request.json['password']
        isAdmin = request.json['admin']
        email = request.json['email']
        nif = request.json['nif']
        data = (request.json['datanasc'])[:10]

        code = userController.register_user(username, name, password, isAdmin, email, nif, data)
        if (code == 200):
            return {"sucess": "User registado com sucesso"}
        else:
            return {"error": "Erro a inserir user. Tente outra vez mais tarde"}

    else:
        return {"error": "Name ja utilizado."}


# rota usada para fazer log in de um user
@app.route('/user/login', methods=['POST'])
def login():
    username = request.json['username']
    password = request.json['password']

    logIn = userController.checkcredentials(username, password)

    if logIn == "0":
        return {"error": "Credenciais erradas"}

    else:
        return {"sucess": "Log in com sucesso"}


@app.route('/user/wallet/<user>')
def getwallet(user):
    return userController.getwallet(userController.idfromname(user))


@app.route('/user/bets/<user>')
def checkbets(username):
    userController.checkBets()


@app.route('/user/delete/<user>')
def deleteuser(user):
    return userController.deleteacount(userController.idfromname(user))


@app.route('/user/bethistory/<user>')
def checkbethistory(username):
    return userController.checkBets(userController.idfromname(username))


@app.route('/user/deposit', methods=['POST'])
def deposit():
    return userController.addcredito(request.json['moeda'], request.json['valor'], userController.idfromname(request.json['nome']))


@app.route('/user/withdrawl', methods=['POST'])
def withdrawl():
    return userController.removecredito(request.json['moeda'], request.json['valor'], userController.idfromname(request.json['nome']) )


@app.route('/user/trade', methods=['POST'])
def trade():
    return userController.exchangemoeda(request.json['moeda1'], request.json['moeda2'], request.json['valor'],
                                        userController.idfromname(request.json['nome']))


@app.route('/user/sports/<user>')
def checksports(username):
    return userController.checksport(userController.idfromname(username))


@app.route('/bet/bets')
def showbets():
    return aposta.getApostas()



@app.route('/bet/make')
def makebet():
    return aposta.fazerAposta(request.json[userController.idfromname(request.json["username"])],
                              request.json["aposta_id"],
                              request.json["resultado"],
                              request.json["moeda_id"],
                              request.json["valor"])


@app.route('/bet/create')
# clube1, clube2, oddsw, oddsd, oddsl, desporto
def createbet():
    clube1 = request.json["clube1"]
    clube2 = request.json["clube2"]
    oddsw = request.json["oddsw"]
    oddsd = request.json["oddsd"]
    oddsl = request.json["oddsl"]
    desporto = request.json["desporto"]
    return aposta.criarAposta(clube1, clube2, oddsw, oddsd, oddsl, desporto)


@app.route('/bet/close')
def closebet():
    return aposta.closeAposta(request.json["aposta_id"], request.json["result"])


if __name__ == '__main__':
    print("teste123")
    app.run(debug=True)
