const toTop = document.querySelector(".to-top");

window.addEventListener("scroll", () => {
    if (window.pageYOffset > 100) {
        toTop.classList.add("active");
    } else {
        toTop.classList.remove("active");
    }
})

function hideSpanButtonRefresh() {
    var wid = $(window).width();
    if (wid < 760) {
        $("#btn-refresh > span").hide();
    }
    else if (wid >= 760) {
        $("#btn-refresh > span").show();
    }
}
$(window).resize(function () {
    hideSpanButtonRefresh();
});