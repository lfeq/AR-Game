const express = require('express');
const multer = require('multer');
const mysql = require('mysql2');
const config = require('./config');
const path = require('path');
const bodyParser = require('body-parser');
const fs = require('fs');

const uploadDirectory = 'uploads';

if (!fs.existsSync(uploadDirectory)) {
  fs.mkdirSync(uploadDirectory);
  console.log('Carpeta "uploads" creada');
}

const app = express();
const port = 3000;

app.use(express.json());
app.use(bodyParser.json({ limit: '10000mb', extended: true }));
app.use(bodyParser.urlencoded({ limit: '10000mb', extended: true, parameterLimit: 5000000 }));

const db = mysql.createConnection(config.database);

db.connect((err) => {
  if (err) {
    console.error('Error al conectar a la base de datos:', err);
  } else {
    console.log('Conexión a la base de datos establecida');
  }
});

const storage = multer.diskStorage({
  destination: (req, file, cb) => {
    cb(null, 'uploads/');
  },
  filename: (req, file, cb) => {
    cb(null, file.originalname);
  }
});

const upload = multer({ storage: storage });

app.post('/subir-archivo', upload.single('archivo'), (req, res) => {
  console.log(req.body);
  console.log(req.file.path);
  const { nombreModelo } = req.body;
  const rutaArchivo = req.file.path;

  const insertQuery = 'INSERT INTO demons (demon_name, file_path) VALUES (?, ?)';
  db.query(insertQuery, [nombreModelo, rutaArchivo], (err, result) => {
    if (err) {
      console.error('Error al guardar en la base de datos:', err);
      res.status(500).json({ error: 'Error al guardar en la base de datos' });
    } else {
      res.json({ mensaje: 'Archivo subido y registrado en la base de datos' });
    }
  });
});

app.get('/descargar-archivo/:nombreModelo', (req, res) => {
  const nombreModelo = req.params.nombreModelo;

  const selectQuery = 'SELECT file_path FROM demons WHERE demon_name = ?';
  db.query(selectQuery, [nombreModelo], (err, result) => {
    if (err) {
      console.error('Error al obtener la ruta del archivo:', err);
      res.status(500).json({ error: 'Error al obtener la ruta del archivo' });
    } else if (result.length === 0) {
      res.status(404).json({ error: 'Modelo no encontrado' });
    } else {
      const rutaArchivo = result[0].file_path;
      res.sendFile(rutaArchivo, { root: __dirname });
    }
  });
});

const ip = '192.168.68.82'
app.listen(port, ip, () => {
  console.log(`Servidor en ejecución en http://${ip}:${port}`);
});
