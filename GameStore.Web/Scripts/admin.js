$("#ajaxSelectSubmit").change(function () {
    $(this.form).submit();
});

$('#AjaxChangeSubmit').change(function () {
    $(this.form).submit();
});

$('#supplyProductAdd').click(function () {
    var res = "";
    var count = $('.supply__product').length;
    var first = $('.supply__product')[0].innerHTML;
    first = first.replace(/\[0\]/g, '[' + count + ']');
    res += '<div class="form-group row mx-0 supply__product">' +
        first +
        '</div>';
    $('#supplyProducts').append(res);
});
