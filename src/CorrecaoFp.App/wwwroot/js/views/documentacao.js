$(document).ready(function () {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);

            setTimeout(() => {
                const posicao = targetElement.getBoundingClientRect();
                const offsetPosition = posicao.top + window.pageYOffset - 80;

                window.scrollTo({
                    top: offsetPosition,
                    behavior: 'smooth'
                });
            }, 0);
        });
    });
});