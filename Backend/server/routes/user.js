const express = require('express');
const router = express.Router();

router.post('/register', (req, res) => {
    const { name, email, password, passwordRepeat } = req.body;
    if (password == passwordRepeat) {
        console.log(`Received data - Name: ${name}, Email: ${email}, Password: ${password}, PasswordRepeat: ${passwordRepeat}`,);
        res.json({ message: 'User registered successfully', data: { name, email, password, passwordRepeat } });
    } else {
        console.log(`Received data - Name: ${name}, Email: ${email}, Password: ${password}, PasswordRepeat: ${passwordRepeat}`,);
        res.json({ message: 'User not registered. Password is not same', data: { name, email, password, passwordRepeat } });
    }
});

module.exports = router;
