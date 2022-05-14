const express = require('express');

const jogadorController = require('../controller/jogador-controller');
const router = express.Router();

router.get(`${process.env.JOGADOR_GET_URI}`, jogadorController.getJogadores);

router.post(`${process.env.JOGADOR_POST_URI}`, jogadorController.addJogador);

module.exports = router;
