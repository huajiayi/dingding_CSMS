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
	if(parent.lastChild == targetElement) {
		parent.appendChild(newElement);
	} else {
		parent.insertBefore(newElement, targetElement.nextSibling);
	}
}
var N = 0;
addLoadEvent(function () { fullData(ContractName, Count, TotalProduct, NoTotalProduct) });
addLoadEvent(function () { fullLog(ProductionerLogJson) });

//填充数据
function fullData(contractName, demand, totalProduc, noTotalProduc) {
    if (noTotalProduc == 0) {
        document.getElementById("btn_production").disabled = "disabled";
    }
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_demand").innerHTML = "需求量：" + demand;
	document.getElementById("lbl_totalProduc").innerHTML = "已生产量：" + totalProduc;
	document.getElementById("lbl_noTotalProduc").innerHTML = "未生产量：" + noTotalProduc;
}

//填充日志
function fullLog(ProductionerLogJson) {
	var log = document.getElementById("log");
	if (ProductionerLogJson == "[]") {
	    log.innerHTML = "尚无日志";
	    $("#log").next().remove();
	} else {
		var logList = document.createElement("section");
		logList.id = "cd-timeline";
		logList.className = "cd-container";
		insertAfter(logList, log);
		var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象  LogDatesJson  
		var ProductionerLog = JSON.parse(ProductionerLogJson.replace(reg, '"'));
		
		if (ProductionerLog.length < 5) {
		    $("#more").remove();
		}
		for (var i = 0; i < ProductionerLog.length; i++) {
		    addLog(ProductionerLog[i].LogName, ProductionerLog[i].ProductionCount, ProductionerLog[i].ProductionDate, ProductionerLog[i].LogDate, ProductionerLog[i].Name);
		}
	}
}

//添加日志
function addLog(logName, log, date, logDate, name) {
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
    var h3 = document.createElement("h3");
    h3.innerHTML = "产品:" + logName;
    var p_log = document.createElement("p");
    p_log.innerHTML = "生产数量：" + log + " 套/个";
    var p_date = document.createElement("p");
    p_date.innerHTML = "生产日期：" + date;
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = logDate + " by " + name;
    div_content.appendChild(h3);
    div_content.appendChild(p_log);
    div_content.appendChild(p_date);
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}

function lazyLoad() {
    N = N + 5;
    $.ajax({
        type: "POST",
        url: "/Production/ProductionAjaxTT?ID=" + N,
        async: true,
        cache: false,
        success: function (data) {
            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象  LogDatesJson  
            var ProductionerLog = JSON.parse(data.replace(reg, '"'));
           
            if (ProductionerLog.length < 5) {
                $("#more").remove();
            }
            if (data == "[]") {
                N -= 5;
                alert("暂无最新数据！");
                return false;
            }
            for (var i = 0; i < ProductionerLog.length; i++) {
                if (!document.getElementById("cd-timeline")) {
                    return false;
                }
                addLog(ProductionerLog[i].LogName, ProductionerLog[i].ProductionCount, ProductionerLog[i].ProductionDate, ProductionerLog[i].LogDate, ProductionerLog[i].Name);
            }
        },
        error: function () {
            alert("请求失败");
        }
    });
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