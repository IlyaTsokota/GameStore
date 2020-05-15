$('#deliveryOption').change(function () {
    $('#Address').val("");
    const company = $(this).children('option:selected').val();
    $('.company').addClass('d-none');
    $('.company input').val('');
    if (company === "NovaPoshta") {
        $('#NovaPoshta').toggleClass('d-none');
    }
});

$('#deliveryType').change(function () {
    $('#Address').val("");
    const type = $(this).children('option:selected').val();
    $('.delivery__type').addClass('d-none');
    if (type === "ToDepartment") {
        $('.to__department').toggleClass('d-none');
    } else if (type === "ToAddress") {
        $('.to__address').toggleClass('d-none');
    }
});

$('#NovaPoshtaCities').select2({
    theme: "bootstrap4",
    placeholder: "Город",
    delay: 250,
    minimumInputLength: 2,
    ajax: {
        type: 'POST',
        dataType: 'json',
        url: 'https://api.novaposhta.ua/v2.0/json/',
        contentType: 'application/json',
        data: function (params) {
            let query = JSON.stringify({
                "apiKey": "4e9e7b04e1db07024bf07763f0c542eb",
                "modelName": "Address",
                "calledMethod": "getCities",
                "methodProperties":
                {
                    "FindByString":
                        params.term
                }
            });
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result.data,
                    function (obj) {
                        return { id: obj.Ref, text: obj.DescriptionRu };
                    })
            }
        },
        cache: true
    }
});

$('#NovaPoshtaCities').on('change',
    function () {
        $('#Address').val("");
        $('#NovaPoshtaOffices').empty();
        var ref = $(this).select2('val');
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: 'https://api.novaposhta.ua/v2.0/json/',
            contentType: 'application/json',
            data: JSON.stringify({
                "apiKey": "4e9e7b04e1db07024bf07763f0c542eb",
                "modelName": "AddressGeneral",
                "calledMethod": "getWarehouses",
                "methodProperties": {
                    "Language": "ru",
                    "CityRef": ref
                }
            }),
            success: function (result) {
                $('#NovaPoshtaOffices').append('<option></option>');
                result.data.forEach(function (item) {
                    $('#NovaPoshtaOffices').append('<option class="list-item">' +
                        item.DescriptionRu +
                        '</option>');
                });
            }
        });
    });

$('#NovaPoshtaOffices').select2({
    theme: "bootstrap4",
    placeholder: "Отделение",
    allowClear: true
});

$('#NovaPoshtaOffices').on('change',
    function () {
        let address = GetAddress();
        let city = $('#NovaPoshtaCities').select2('data')[0].text;
        let office = $('#NovaPoshtaOffices').select2('data')[0].text;
        address += 'г.' + city + ', ' + office;
        $('#Address').val(address);
    });

$('.to__address').change(function () {
    let address = GetAddress();
    let city = $('#NovaPoshtaCities').select2('data')[0].text;
    let street = $('#street').val();
    let house = $('#house').val();
    let apartment = $('#apartment').val();
    address += 'г.' + city + ' ул.' + street + ' д.' + house + ' кв.' + apartment;
    $('#Address').val(address);
});

function GetAddress() {
    let company = $('#deliveryOption option:selected').text();
    let type = $('#deliveryType option:selected').text();
    let res = company + ", " + type + ", ";
    return res;
}