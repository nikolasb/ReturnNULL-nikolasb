//$(document).ready(function () {
//    var bill_id = $('#blid').val();
//    var bltitle = $('#bltitle').val();
//    var com = $('#comment').val();
//    var uid = $('#uid').val();
//    $('#create').click(function () {
//        console.log("bill: " + bill_id + "comment: " + com + "user: " + uid + "bill title: " + bltitle);
//        //$.post('http://localhost:53764/Home/CreateComments?blid=' + bill_id + '&bltitle=' + bltitle + '&comment=' + com, function (data) { });

//        $.ajax({
//            url: 'CreateComments?blid=' + bill_id + '&bltitle=' + bltitle + '&uid=' + uid + '&comment=' + com + 'haha/',
//            type: 'POST',
//            success: function (data) { console.log("success"); },
//            error: function () { alert("data load faild");}
//        });
//    });
//});