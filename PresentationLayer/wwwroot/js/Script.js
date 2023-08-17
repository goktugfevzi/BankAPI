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


    $("#UpdateCardNumber").val(cardNumber);
    $("#expiryYear").val(ExpiryYear);
    $("#expiryMonth").val(ExpiryMonth);
    $("#UpdateHolderName").val(holderName);
    $("#editcvvNumber").val(CVV);
    $("#CardType").val(cardType);
    $("#CardId").val(cardId);

  
});

$(document).on('touchstart click', '#BtnCardAdd', function () {    
    var modal = document.getElementById("add-new-card-details");
    $.ajax({
        type: "GET",
        url: "/User/GenerateCardNumber",
        success: function (data) {
            $(modal).modal("show");

            var firstFourDigits = Math.floor(Math.random() * (5301 - 5100)) + 5100;
            var remainingDigits = "";
            for (var i = 0; i < 12; i++) {
                remainingDigits += Math.floor(Math.random() * 10);
            }
            var generatedCardNumber = firstFourDigits.toString() + remainingDigits;

            $("#cardNumber").val(generatedCardNumber);
            $("#month").val(data[1]);
            $("#year").val(data[2]);
            $("#cvvNumber").val(data[3]);
            $("#cardHolderName").val(data[4]);          
            
        },
        error: function () {
            swal("Hata", "Kart Numarası Oluşturulurken Bir Hata Oluştu", "error");
        }
    });
});

$(document).on('touchstart click', '#BtnDeposit', function () {
    var formData = $("#FrmDeposit").serialize();
    $.ajax({
        type: "POST",
        url: "/Transaction/DepositMoney",
        data: formData,
        success: function (data) {
            if (data.sonuc) {
                swal({
                    title: "Tebrikler!",
                    text:"Hesabınıza Para Yatırıldı",
                    icon: "success",
                    buttons: {
                        tamam: "Tamam",                        
                    },
                }).then((value) => {
                    switch (value) {
                        case "tamam":
                            window.location.href = "/Default/Index/"
                            break;
                        default:
                            break;
                    }
                });
            }
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
            if (data.sonuc) {
                window.location.href="/Default/Index/"
            }
            
        },
        error: function () {
            swal("Hata", "Fatura Ödeme İşleminde Bir Hata Oluştu", "error");
        }
    });
});

//// HTML'deki input alanını seçiyoruz
//var inputElement = document.getElementById('youSend');

//// Yatırılacak miktarı gösteren span elementini seçiyoruz
//var yatirilacakMiktarElement = document.getElementById('depositAmount');

//// Input alanına her yazıldığında tetiklenecek fonksiyon
//inputElement.addEventListener('input', function () {
//    var enteredAmount = parseFloat(inputElement.value.replace(',', '.')); 
//    var yatirilacakMiktar = enteredAmount.toFixed(2);
//    yatirilacakMiktarElement.textContent = yatirilacakMiktar + ' TRY'; 
//});


//var inputElement2 = document.getElementById('youSendmoney');
//var gonderilecekMiktarElement2 = document.getElementById('postingfee');
//var toplamtutar = document.getElementById('TotalPrice');
//inputElement2.addEventListener('input', function () {
//    var enteredAmount = parseFloat(inputElement2.value.replace(',', '.'));
//    var yatirilacakMiktar = enteredAmount.toFixed(2);
//    var toplam = (parseInt(toplamtutar) + 35).toFixed(2);
//    yatirilacakMiktarElement2.textContent = yatirilacakMiktar + ' TRY';
//    toplamtutar.textContent = toplam + ' TRY';
//});




