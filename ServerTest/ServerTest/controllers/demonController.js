const Demon = require('../models/demon');

module.exports = {
    register(req, res){
        const demon = req.body;
        Demon.create(demon, (err, data) => {
            if(err){
                return res.status(501).json({
                    success: false,
                    message: 'Todo se fue al demonio',
                    error: err
                });
            }
            return res.status(201).json({
                success: true, 
                message: 'Todo salgo de poca madre',
                data: data
            })
        });
    }
}