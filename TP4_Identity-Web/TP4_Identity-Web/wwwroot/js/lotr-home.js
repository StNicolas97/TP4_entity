// lotr-home.js — Scroll reveal via IntersectionObserver
(function () {
    'use strict';

    var revealEls = document.querySelectorAll('.lotr-reveal');
    if (!revealEls.length || !('IntersectionObserver' in window)) {
        // Fallback: make all elements visible immediately
        for (var i = 0; i < revealEls.length; i++) {
            revealEls[i].classList.add('is-visible');
        }
        return;
    }

    var observer = new IntersectionObserver(
        function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('is-visible');
                    observer.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.1, rootMargin: '0px 0px -40px 0px' }
    );

    revealEls.forEach(function (el) {
        observer.observe(el);
    });
})();
