
export function moveElementOnScroll(element) {
    const mainContent = document.getElementsByClassName('main-content')[0];
    if (mainContent) {
        mainContent.addEventListener('scroll', function() {
            element.style.transform = `translateY(${mainContent.scrollTop}px)`;
        });
    } else {
        setTimeout(moveElementOnScroll, 100);
    }
    
}