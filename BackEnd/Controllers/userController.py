import mysql.connector
from mysql.connector import Error


def connect_db():
    return create_server_connection("localhost", "root", "pass", "mydb")


# criar ligação com a base de dados
def create_server_connection(host_name, user_name, user_password, db_name):
    connection = None
    try:
        connection = mysql.connector.connect(
            host=host_name,
            user=user_name,
            passwd=user_password,
            database=db_name
        )
        print("MySQL Database connection successful")
    except Error as err:
        print(f"Error: '{err}'")

    return connection


# usar esta função quando for para executar uma query e só queremos saber se correu bem (200) ou mal (400)
def execute_query(connection, query):
    cursor = connection.cursor()
    try:
        cursor.execute(query)
        connection.commit()
        return 200
    except Error as err:
        return 400


# Usar esta função quando for para executar uma query e receber um resultado. Neste caso, o resultado vai ser
# devolvido na variavel result
def read_query(query):
    connection = connect_db()
    cursor = connection.cursor()
    result = None
    try:
        cursor.execute(query)
        return cursor.fetchall()
    except Error as err:
        return 400


def idfromname(username):
    queryUser = f'''
                SELECT user_id FROM mydb.user WHERE username = "{username}";
            '''
    return read_query(queryUser)


# função a usar para registar um user
def register_user(username, name, password, isadmin, email, nif, dn):
    connection = connect_db()
    query = f'''
        INSERT INTO mydb.user (username, name, password, isAdmin, email, nif, data_nascimento) 
        VALUES  ('{username}', '{name}', '{password}', {isadmin}, '{email}', '{nif}', '{dn}');
    '''
    execute_query(connection, query)

    queryMoeda = f'''
        SELECT moeda_id FROM mydb.moeda;
    '''

    user_id = idfromname(username)

    for x in read_query(queryMoeda):
        query = f'''
            INSERT INTO mydb.userMoeda (user_id, moeda_id, quantidade)
            VALUES ({user_id}, {x}, 0);
        '''
        # se der erro aqui tentar com x[0] e depois x[0][0]

    execute_query(connection, query)

    return 200


# função a usar para receber a lista de todos os users e os seus dados
def getusers():
    connection = connect_db()
    query = f'''
        SELECT * FROM mydb.user;
    '''
    return read_query(query)


# função a usar para receber os dados de um user dado o ser name
def getuser(username):
    connection = connect_db()
    query = f'''
        SELECT * FROM mydb.user WHERE user.username = "{username}";
    '''
    return read_query(query)


# função que verifica que se já existe na bd um user com um dado name, util para verificar quando alguem tentar fazer
# um registo
def checkuserexists(username):
    result = getuser(username)
    if not result:  # veio resultado vazio, ou seja, nao existe uma entrada na bd com este name
        return "0"

    return "1"


# funcao que verifica se as credenciais estão corretas. Para ser usada no log in
def checkcredentials(name, password):
    connection = connect_db()
    query = f'''
        SELECT * FROM mydb.user WHERE (user.name = "{name}") AND (user.password = "{password}");
    '''
    result = read_query(query)
    if not result: return "0"

    return "1"


# funcao que devolve a wallet do utilizador
def getwallet(user_id):
    query = f'''
        SELECT moeda.nome,userMoeda.quantidade FROM mydb.userMoeda INNER JOIN moeda ON moeda.moeda_id=userMoeda.moeda_id WHERE userMoeda.user_id={user_id}
    '''

    lista = read_query(query)
    result = {}
    for x in lista:
        tmp = {x[0]: x[1]}
        result.update(tmp)

    return result


# função que verifica se um dado user tem dinheiro para fazer a aposta que pretende
def checkcredito(moeda_id, valor, user_id):
    query = f'''
        SELECT quantidade FROM mydb.userMoeda WHERE id = {user_id} AND moeda_id = {moeda_id}
    '''

    valorBD = (read_query(query)[0])[0]

    if valorBD < valor:
        return "0"

    return "1"


def addcredito(moeda_id, valor, user_id):
    connection = connect_db()

    query = f'''
            SELECT valor FROM mydb.userMoeda WHERE user_id = {user_id} AND moeda_id = {moeda_id}
        '''

    valorBD = (read_query(query)[0])[0]

    query = f'''
        UPDATE mydb.userMoeda SET valor={valorBD + valor} WHERE user_id={user_id} AND moeda_id={user_id};
    '''
    return "1"


def removecredito(moeda_id, valor, user_id):
    connection = connect_db()

    query = f'''
            SELECT quantidade FROM mydb.userMoeda WHERE user_id = {user_id} AND moeda_id = {moeda_id}
        '''

    valorBD = (read_query(query)[0])[0]

    if valorBD < valor:
        return "0"

    query = f'''
        UPDATE mydb.userMoeda SET valor={valorBD - valor} WHERE user_id={user_id} AND moeda_id={user_id};
    '''
    return "1"


def exchangemoeda(moeda1_id, moeda2_id, valor, user_id):
    if not removecredito(moeda1_id, valor, user_id):
        return "0"

    query = f'''
                SELECT rate_to_euro FROM mydb.moeda WHERE moeda_id = {moeda1_id}
            '''

    query2 = f'''
                SELECT rate_from_euro FROM mydb.moeda WHERE moeda_id = {moeda2_id}
            '''

    # se der erro aqui tentar sem [0] e com [0][0]
    mult = read_query(query)[0]
    mult = mult * read_query(query2)[0]

    return addcredito(moeda2_id, valor * mult, user_id)


def deleteacount(userid):
    query = f'''
        DELETE FROM mydb.user WHERE user.user_id={userid}
        '''

    execute_query(query)


def checkBets(userid):
    # query2 = f'''
    #           SELECT aposta.* from aposta,apostaComposta,userApostaColectiva
    #           WHERE apostaComposta.aposta_id = aposta.id AND apostaComposta.userApostaColetiva_id=userApostaColectiva.apostaColetiva_id AND userApostaColectiva.user_id={userid}
    #       '''
    query = f'''
            SELECT * FROM mydb.aspostaComposta WHERE mydb.apostaComposta.userApostaColetiva_id=mydb.userApostaColectiva.apostaColetiva_id AND mydb.userApostaColectiva.user_id={userid} 
        '''

    lista = read_query(query)
    result = {}
    for l in lista:
        tmp = {l[0]: {
            "resultado": l[1],
            "amount": l[2],
        }}
        result.update(tmp)

    return result


def checksport(userid):
    query = f'''
                SELECT aposta.desporto FROM mydb.aposta, mydb.aspotaComposta, mydb.userApostaColetiva 
                WHERE aposta.id =apostaComposta.aposta_id AND apostaComposta.userApostaColetiva_id=userApostaColectiva.apostaColetiva_id AND userApostaColectiva.user_id={userid} 
            '''

    lista = read_query(query)
    result = {}
    for l in lista:
        tmp = {l[0]: l[0]}
        result.update(tmp)

    return result
