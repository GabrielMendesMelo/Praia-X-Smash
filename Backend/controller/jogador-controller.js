const HttpError = require('../models/http-error');

const Jogador = require('../models/jogador');

const MAX_RANKING = 50;

const getJogadores = async (req, res, next) => {
    let jogadores;
    try {
        jogadores = await Jogador
        .find()
        .sort({ pontuacao: -1});
    } catch (e) {
        return next (new HttpError('GET: Jogador.find() falhou', 500));
    }

    try {
        if (jogadores.length > MAX_RANKING) {
            for (i = jogadores.length - 1; i > MAX_RANKING - 1; i--) {
                await jogadores[i].remove();
            }
        }    
    } catch (e) {
        return next(new HttpError('GET: Jogador.remove() falhou', 500));
    }
            
    res.json({ jogadores: jogadores.map(jogador => jogador.toObject({ getters: true })) });
};

const addJogador = async (req, res, next) => {
    const { nome, pontuacao } = req.body;

    const jogadorCriado = new Jogador({
        nome,
        pontuacao
    });

    try {
        await jogadorCriado.save();
    } catch (e) {
        return next(new HttpError('POST: Jogador.save() falhou', 500));
    }

    res.status(201).json({
        meuId: jogadorCriado.meuId,
        nome: jogadorCriado.nome,
        pontuacao: jogadorCriado.pontuacao
    });
};

exports.getJogadores = getJogadores;
exports.addJogador = addJogador;
