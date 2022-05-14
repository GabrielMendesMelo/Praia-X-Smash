class HttpError extends Error {
    constructor(mensagem, erroCod) {
        super(mensagem);
        this.cod = erroCod;
    }
}

module.exports = HttpError;
