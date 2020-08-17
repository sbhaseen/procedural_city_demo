const gameInstance = UnityLoader.instantiate(
  'gameContainer',
  'Build/Build - Web.json',
  { onProgress: UnityProgress }
);

const navbarToggle = document.querySelector('.navbar-toggler');
const navbarMenu = document.querySelector('.navbar-menu');

if (navbarToggle && navbarMenu) {
  navbarToggle.addEventListener('click', () => {
    navbarMenu.classList.toggle('is-active');
  });

  navbarMenu.addEventListener('click', () => {
    navbarMenu.classList.remove('is-active');
  });
}
