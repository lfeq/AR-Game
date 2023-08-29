const mysql = require('mysql');

const db = mysql.createConnection({
    host:'localhost',
    user:'root',
    password:'admin123',
    database:'demons'
});

db.connect(function(err){
    if(err) throw err;
    console.log('AHORA ESTA VIVA LA BASE DE DATOS!!!!!');
});

module.exports = db;