//toggle sidebar
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
    //resizeWidth();
});

////Resize map when sidebar closes
//$(resizeHeight);
//$(resizeWidth);
//$(window).resize(resizeHeight);
//$(window).resize(resizeWidth);
//function resizeHeight() {
//    var $window = $(window),
//        $nav = $('#nav'),
//        $content = $('#wrapper'),
//        $map = $('#map');
//    $content.height($window.height() - $nav.height());
//    $map.height($content.height());
//}
//function resizeWidth() {
//    var $window = $(window),
//        $nav = $('#nav'),
//        $content = $('#wrapper'),
//        $map = $('#map'),
//    $sideBar = $('#sidebar-wrapper');
//    if ($content.is("toggled"))
//    {
//        $map.width($window.width()-500);
//    }else{
//        $map.width($window.width()-$sideBar.width());
//    }
    
//    $map.height($content.height());
//}