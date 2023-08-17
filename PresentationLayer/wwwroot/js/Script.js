$(document).on('touchstart click', '#BtnCardDetail', function () {

    var cardNumber = $(this).data('cardnumber');
    var holderName = $(this).data('holder');
    var ExpiryMonth = $(this).data('month');
    var ExpiryYear = $(this).data('year');
    var cardType = $(this).data('cardtype');
    var cardId = $(this).data('cardid');

    if (cardType == "MasterCard") {
        var image = document.getElementById("cardlogo");
        image.src = "/bank-template/images/mastercard.png";
    }

    var CVV = $(this).data('cvv');
    var modal = document.getElementById("edit-card-details");
    $(modal).modal("show");

    // jQuery seçicileri düzgün bir şekilde kullanmalısınız.
    $("#UpdateCardNumber").val(cardNumber);
    $("#expiryYear").val(ExpiryYear);
    $("#expiryMonth").val(ExpiryMonth);
    $("#UpdateHolderName").val(holderName);
    $("#editcvvNumber").val(CVV);
    $("#CardType").val(cardType);
    $("#CardId").val(cardId);


});

$(document).on('touchstart click', '#BtnUpdateCard', function () {
    debugger;
    var formData = $("#updateCard").serialize();
    $.ajax({
        type: "POST",
        url: "/User/UpdateCard",
        data: formData,
        success: function (data) {
            if (data.sonuc) {
                var modal = document.getElementById("edit-card-details");
                $(modal).modal("hide");
                window.location.href = "/User/MyCards"
            }
            else {
                swal("Hata", "Kart Güncelleme İşleminde Bir Hata Oluştu", "error");
            }

        },
        error: function () {
            swal("Hata", "Yorum Ekleme İşleminde Bir Hata Oluştu", "error");
        }
    });
});

$(document).on('touchstart click', '#BtnCardAdd', function () {
    var modal = document.getElementById("add-new-card-details");
    $.ajax({
        type: "GET",
        url: "/User/GenerateCardNumber",
        success: function (data) {
            debugger;
            var cardDetails = data;
            $(modal).modal("show");

            $("#cardNumber").val(cardDetails[0]);
            $("#month").val(cardDetails[1]);
            $("#year").val(cardDetails[2]);
            $("#cvvNumber").val(cardDetails[3]);
            $("#cardHolderName").val(cardDetails[4]);

        },
        error: function () {
            swal("Hata", "Kart Numarası Oluşturulurken Bir Hata Oluştu", "error");
        }
    });

});


$(document).on('touchstart click', '#payBillButton', function () {
    var selectedOption = document.getElementById('cardType').options[document.getElementById('cardType').selectedIndex];
    var selectedBillID = selectedOption.value;
    $.ajax({
        type: "POST",
        url: "/Bill/Index",
        data: {
            BillID: selectedBillID
        },
        success: function (data) {
            console.log("İşlem Başarılı");
        },
        error: function () {
            swal("Hata", "Fatura Ödeme İşleminde Bir Hata Oluştu", "error");
        }
    });
});