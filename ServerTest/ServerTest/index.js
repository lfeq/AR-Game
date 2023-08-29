const express = require('express');
const app = express();
const http = require('http');
const server = http.createServer(app);
const logger = require('morgan');
const cors = require('cors');
const demonRoutes = require('./routes/demonRoutes');

const port = process.env.PORT || 3000;
console.log(process.env.SERVER_IP);

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({
    extended: true
}));

app.use(cors());
demonRoutes(app);

app.disable('x-powered-by');

app.set('port', port);
server.listen(3000, '192.168.68.81', function(){
    console.log('ESTA VIVOOOOOOOOOOOO!!!!!! ' + process.pid + ' Iniciada...')
});

app.get('/', (req, res) => {
    res.send('Soy Fan de MH');
});

app.use((err, req, res, next) => {
    console.log(err);
    res.status(err.status || 500).send(err.stack);
})