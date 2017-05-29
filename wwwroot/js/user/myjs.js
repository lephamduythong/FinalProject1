$('#bxh_show_home > div:eq(1)').removeClass('hidden');

$('#grade_select_home a').click(function () {
    $('#grade_select_home .btn-warning').removeClass('btn-warning');
    $(this).addClass('btn-warning');
    // $('#test').text(grade_button_index);
    $('#bxh_show_home > div').not('div[class*="hidden"]').addClass('hidden');
    $('#bxh_show_home > div:eq(' + $(this).index() + ')').removeClass('hidden');
});


$('#nav_grade').click(function() {
    $(this).children('ul').slideDown(200);
}, function() {
    $(this).children('ul').slideUp(200);
});

$('#nav_exercise').click(function () {
    $(this).children('ul').slideDown(200);
}, function () {
    $(this).children('ul').slideUp(200);
});

//$('div').not('#nav_grade').click(function () {
//    $('#nav_grade ul').slideUp();
//});






