const demonsController = require('../controllers/demonController');

module.exports = (app) => {
    app.post('/api/demon/create', demonsController.register);
}