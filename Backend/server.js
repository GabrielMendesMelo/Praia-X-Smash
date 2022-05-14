const express = require('express');
const mongoose = require('mongoose');

const jogadorRoutes = require('./routes/jogador-routes');
const HttpError = require('./models/http-error');

const app = express();

app.use(express.json());

app.use((req, res, next) => {
    res.setHeader(
        'Access-Control-Allow-Origin',
        '*'
    );
    res.setHeader(
        'Access-Control-Allow-Headers',
        'Origin, X-Requested-With, Content-Type, Accept, Authorization'
    );
    res.setHeader(
        'Access-Control-Allow-Methods',
        'GET, POST, DELETE'
    );
    next();
});

app.use(`${process.env.SERVER_URI}`, jogadorRoutes);

app.use((req, res, next) => {
    throw new HttpError('Não foi possível encontrar esse caminho.', 404);
});

app.use((error, req, res, next) => {
    if (res.headerSent) {
        return next(error);
    }
    res.status(error.code || 500);
    res.json({ mensagem: error.message || 'Um erro inesperado aconteceu!' });
});

mongoose
    .connect(`mongodb+srv://${process.env.DB_USER}:${process.env.DB_PASSWORD}@cluster0.thzr4.mongodb.net/${process.env.DB_NAME}?retryWrites=true&w=majority`)
    .then(() => {
        app.listen(process.env.PORT)
    })
    .catch(e => {
        console.log(e);
    });
