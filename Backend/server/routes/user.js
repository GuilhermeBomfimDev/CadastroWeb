const express = require('express');
const { body, validationResult } = require('express-validator');
const router = express.Router();

router.post('/register', [
    body('name').isLength({ min: 5}).withMessage('Nome é obrigatório'),
    body('email').isEmail().withMessage('Digite um email válido'),
    body('password').isLength({ min: 6}).withMessage('Senha deve ter mais de 6 caracteres') 
], (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() });
    }

    const { name, email, password, passwordRepeat } = req.body;
    if (password == passwordRepeat) {
        console.log(`Received data - Name: ${name}, Email: ${email}, Password: ${password}, PasswordRepeat: ${passwordRepeat}`,);
        res.json({ message: 'User registered successfully', data: { name, email, password, passwordRepeat } });
    } else {
        console.log(`Received data - Name: ${name}, Email: ${email}, Password: ${password}, PasswordRepeat: ${passwordRepeat}`,);
        res.json({ message: 'Usuário não registrado. As senhas estão diferentes', data: { name, email, password, passwordRepeat } });
    }
});

module.exports = router;
