const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const jogadorSchema = new Schema({
    nome: { type: String, required: true },
    pontuacao: { type: Number, required: true }
});

module.exports = mongoose.model('Jogador', jogadorSchema);
