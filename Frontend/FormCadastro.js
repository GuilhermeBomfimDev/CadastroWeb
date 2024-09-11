document.getElementById('userForm').addEventListener('submit', async function(event) {
    event.preventDefault(); // Evita o envio padrão do formulário

    const nome = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const telefone = document.getElementById('telefone').value;
    const password = document.getElementById('password').value;
    const passwordRepeat = document.getElementById('passwordRepeat').value;

    const checkEmailResponse = await fetch(`https://localhost:7005/api/UserDados/email/${email}`, { // Atualize a URL conforme necessário
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const checkPasswordResponse = await fetch (`https://localhost:7005/api/UserDados/password/${password}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const messageDiv = document.getElementById('message');

    // Verifica se o email já está cadastrado
    if (checkEmailResponse.ok) {
        const data = await checkEmailResponse.json();
        
        if (data.exists) {
            messageDiv.innerText = 'O e-mail informado já está cadastrado.';
            messageDiv.className = 'error';
            messageDiv.style.display = 'block';
            return; // Para o processo, não faz o POST
        }
    } else {
        messageDiv.innerText = 'Erro ao verificar o e-mail.';
        messageDiv.className = 'error';
        messageDiv.style.display = 'block';
        return;
    }

    // Verifica se as senhas inseridas são iguais para validação
    if (password != passwordRepeat) {
        messageDiv.innerText = 'As senhas inseridas precisas ser iguais.';
        messageDiv.className = 'error';
        messageDiv.style.display = 'block';
        return;
    }

    // Verifica se existe senha igual no banco de dados
    if (checkPasswordResponse.ok) {
        const dataPassword = await checkPasswordResponse.json();

        if (dataPassword.exists) {
            messageDiv.innerText = 'A senha digitada já existe.';
            messageDiv.className = 'error';
            messageDiv.style.display = 'block';
            return;
        } 
    } else {
        messageDiv.innerText = 'Erro ao verificar a senha.';
        messageDiv.className = 'error';
        messageDiv.style.display = 'block';
        return;
    }

    // Se o e-mail não existir, faz o POST 
    const response = await fetch('https://localhost:7005/api/UserDados', { // Atualize a URL se necessário
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            nome: nome,
            email: email,
            telefone: telefone,
            password: password
        })
    });

    if (response.ok) {
        messageDiv.innerText = 'Usuário adicionado com sucesso!';
        messageDiv.className = 'success';
        messageDiv.style.display = 'block';

        setTimeout(() => {
            window.location.href = 'LandingPage.html'; //Leva para outra página
        }, 2000);
    } else {
        messageDiv.innerText = 'Erro ao adicionar usuário.';
        messageDiv.className = 'error';
        messageDiv.style.display = 'block';
    }
});