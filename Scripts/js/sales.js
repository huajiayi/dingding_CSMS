function addLoadEvent(func) {
	var oldonload = window.onload;
	if(typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

function insertAfter(newElement, targetElement) {
    var parent = targetElement.parentNode;
    if (parent.lastChild == targetElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement, targetElement.nextSibling);
    }
}
<<<<<<< HEAD
var N=0;
=======

>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
addLoadEvent(function () { fullData(ContractName, Contract_Amount, AmountCollection, NoAmountCollection) });
addLoadEvent(function () { fullLog(SalesLogJson) });
//addLoadEvent(function () { hasLog(SalesLogJson) });
//判断有没有日志，没有则显示尚无日志
//function hasLog(haslog) {
   
//    var log = document.getElementById("log");
//    if (haslog=="[]") {
//        log.innerHTML = "尚无日志";
//    } else {
//        var logList = document.createElement("section");
//        logList.id = "cd-timeline";
//        logList.className = "cd-container";
//        insertAfter(logList, log);
//    }
//}

//填充数据
<<<<<<< HEAD
function fullData(contractName, contractAmount, amountReceived, amountNotReceived) {
    if (amountNotReceived==0) {
        document.getElementById("btn_amountCollection").disabled = "disabled";
    }
=======
function fullData(contractName, contractAmount, amountReceived, amountNotReceived){
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_contractAmount").innerHTML = "总金额：" + contractAmount;
	document.getElementById("lbl_amountReceived").innerHTML = "已收金额：" + amountReceived;
	document.getElementById("lbl_amountNotReceived").innerHTML = "未收金额：" + amountNotReceived;
}

//填充日志ddddd
function fullLog(haslog) {
    var log = document.getElementById("log");
    if (haslog == "[]") {
        log.innerHTML = "尚无日志";
    } else {
        var logList = document.createElement("section");
        logList.id = "cd-timeline";
        logList.className = "cd-container";
        insertAfter(logList, log);
<<<<<<< HEAD
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象 ll   
        var SalesLog = JSON.parse(SalesLogJson.replace(reg, '"'));
        var aa = SalesLog.length - 1
        var SalesLogID=SalesLog[aa].ID;
=======
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var SalesLog = JSON.parse(SalesLogJson.replace(reg, '"'));
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
        for (var i = 0; i < SalesLog.length; i++) {
            addLog(SalesLog[i].LogName, SalesLog[i].Service, SalesLog[i].AffirmIncomeAmount, SalesLog[i].AffirmIncomeDate, SalesLog[i].LogDate, SalesLog[i].Name);
        }
    }
}


//添加日志
function addLog(logName, service, log, date, logDate, name) {
    var logList = document.getElementById("cd-timeline");
    var div_block = document.createElement("div");
    div_block.className = "cd-timeline-block";
    var div_img = document.createElement("div");
    div_img.className = "cd-timeline-img cd-picture";
    var img = document.createElement("img");
    img.src = "../../Content/img/cd-icon-picture.svg";
    div_img.appendChild(img);
    div_block.appendChild(div_img);
    var div_content = document.createElement("div");
    div_content.className = "cd-timeline-content";
<<<<<<< HEAD
    var h3 = document.createElement("h3");
    h3.innerHTML = "收款原因:" + logName;
    var p_service = document.createElement("p");
    p_service.innerHTML = "服务款项：" + service;
    var p_log = document.createElement("p");
    p_log.innerHTML = "收款金额：" + log + " 元";
    var p_date = document.createElement("p");
    p_date.innerHTML = "收款日期：" + date;
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = logDate + " by " + name;
    div_content.appendChild(h3);
    div_content.appendChild(p_service)
    div_content.appendChild(p_log);
    div_content.appendChild(p_date);
=======
    var h2 = document.createElement("h2");
    h2.innerHTML = logName;
    var h3_service = document.createElement("h3");
    h3_service.innerHTML = service;
    var h3 = document.createElement("h3");
    h3.innerHTML = "收款 " + log + " 元";
    var h3_date = document.createElement("h3");
    h3_date.innerHTML = "收款日期：<br/>" + date;
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = logDate + " by " + name;
    div_content.appendChild(h2);
    div_content.appendChild(h3_service)
    div_content.appendChild(h3);
    div_content.appendChild(h3_date);
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}
<<<<<<< HEAD

var scrollTop;
window.onscroll = function () {
   
    totalheight = parseFloat($(window).height()) + parseFloat($(window).scrollTop());
    if (($(document).height()) <= totalheight) {
        scrollTop = document.body.scrollTop;
        N = N + 5;
        $.ajax({
            type: "POST",
            url: "/ContractandSales/SalesAjaxTT?ID="+N,
            async: true,
            cache: false,
            success: function (data) {
                var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                var SalesLog = JSON.parse(data.replace(reg, '"'));
                for (var i = 0; i < SalesLog.length; i++) {
                    if (!document.getElementById("cd-timeline")) {
                        return false;
                    }
                    addLog(SalesLog[i].LogName, SalesLog[i].Service, SalesLog[i].AffirmIncomeAmount, SalesLog[i].AffirmIncomeDate, SalesLog[i].LogDate, SalesLog[i].Name);
                }
            },
            error: function () {
                alert("请求失败");
            }
        });
      
        document.body.scrollTop = scrollTop;
        var $timeline_block = $('.cd-timeline-block');
        //hide timeline blocks which are outside the viewport
        $timeline_block.each(function () {
            if ($(this).offset().top > $(window).scrollTop() + $(window).height() * 0.75) {
                $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
            }
        });
        //on scolling, show/animate timeline blocks when enter the viewport
        $(window).on('scroll', function () {
            $timeline_block.each(function () {
                if ($(this).offset().top <= $(window).scrollTop() + $(window).height() * 0.75 && $(this).find('.cd-timeline-img').hasClass('is-hidden')) {
                    $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
                }
            });
        });
    }
}
=======
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
