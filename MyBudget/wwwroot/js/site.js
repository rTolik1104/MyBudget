const sideMenu = document.querySelector("aside");

var menuBtn = document.getElementById('menu-btn');
var closeBtn = document.getElementById('close-btn');
const themeToggler = document.querySelector(".theme-toggler");

if (menuBtn) {
    menuBtn.addEventListener('click', () => {
        sideMenu.style.display = 'block';
    });
}

if (closeBtn) {
    closeBtn.addEventListener('click', () => {
        sideMenu.style.display = 'none';
    })
}


if (themeToggler) {
    themeToggler.addEventListener('click', () => {
        document.body.classList.toggle('dark-theme-variables');
        themeToggler.querySelector('span:nth-child(1)').classList.toggle('active');
        themeToggler.querySelector('span:nth-child(2)').classList.toggle('active');
    })
}
