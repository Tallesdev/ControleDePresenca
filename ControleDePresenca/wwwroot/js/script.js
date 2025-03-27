const conteiner = document.querySelector('.conteiner');
const registerBtn = document.querySelector('.register-btn');
const loginBtn = document.querySelector('.login-btn');

registerBtn.addEventListener('click', () => {
    conteiner.classList.add('active'); // muda o conteiner da div para ativo assim fazendo a animacao
});

loginBtn.addEventListener('click', () => {
    conteiner.classList.remove('active'); // remove o ativo voltando o conteiner para login
});