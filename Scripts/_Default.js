
window.onload = function () {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data = JSON.parse(arr.replace(reg, '"'));

    for (var i = 0; i < data.length; i++) {
        $("select").append("<option>" + data[i] + "</option>");
    }
   
   
}
function A () {
    location.href = "/MM/Index";
}
 //$("#submit02").click(function () {
 //       var UName = $("#uname").val();
 //       var Phone = $("#uphone").val();
 //       alert("用户名:" + UName + " 手机号:" + Phone);
 //       $.ajax({
 //           type: "post",
 //           url: "GamePlay.aspx/",
           
 //           contentType: "application/json;charset=utf-8",
 //           dataType: "json",
 //           success: function (r) {
                
 //               alert(r);
 //           },
 //           error: function (e) {
 //               alert("错误是:" + e.responseText);
 //           }
 //       });

 //   })