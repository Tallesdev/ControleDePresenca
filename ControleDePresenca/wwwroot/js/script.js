const container = document.querySelector('.container');
const registerBtn = document.querySelector('.register-btn');
const loginBtn = document.querySelector('.login-btn');

registerBtn.addEventListener('click', () => {
    container.classList.add('active'); // muda o conteiner da div para ativo assim fazendo a animacao
});

loginBtn.addEventListener('click', () => {
    container.classList.remove('active'); // remove o ativo voltando o conteiner para login
});