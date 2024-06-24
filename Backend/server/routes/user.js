const express = require('express');
const router = express.Router();

router.post('/register', (req, res) => {
    const { name, email, password } = req.body;
    console.log(`Received data - Name: ${name}, Email: ${email}, Password: ${password}`);
    res.json({ message: 'User registered successfully', data: { name, email, password } });
});

module.exports = router;
