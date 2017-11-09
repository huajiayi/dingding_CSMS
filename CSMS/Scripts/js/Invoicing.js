function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
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

var N = 0;
var Accountant = new Array();
addLoadEvent(function () { fillServiceZZ(AccountantJson) });
//addLoadEvent(function () { fillService(Accountant) });
//addLoadEvent(function () { fullData(ContractName, Contract_Amount, SubAffirmIncomeAmount, NoAmountCollection, Accountant.AffirmIncomeGist, Accountant.AffirmIncomeAmount, Accountant.SubInvoiceCount, Accountant.SubInvoiceAmount, Accountant.SubCost, Accountant.Subworker, Accountant.SubMaterial, Accountant.Subtotal, Accountant.AvgGrossrofitMargin) });


function fillServiceZZ(AccountantJson) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象   LogDatesJson 
    Accountant = JSON.parse(AccountantJson.replace(reg, '"'));
    fullData(name, Accountant[0].SubInvoiceCount, Accountant[0].SubInvoiceAmount, Accountant[0].InvoicingDate);
    fillService(Accountant);
    fullLog(InvoicingJson)
}
//填充数据 lbl_amount
function fullData(name, count, Amount) {
    document.getElementById("lbl_contractName").innerHTML = "合同名称：" + name;
    document.getElementById("lbl_count").innerHTML = "已开票数量：" + count;
    document.getElementById("lbl_amount").innerHTML = "开票金额：" + Changemoney(Amount);

}

//填充日志
function fullLog(haslog) {
    var log = document.getElementById("log");
    if (haslog == "[]") {
        log.innerHTML = "尚无日志";
        $("#log").next().remove();
    } else {
        var logList = document.createElement("section");
        logList.id = "cd-timeline";
        logList.className = "cd-container";
        insertAfter(logList, log);
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var Invoicing = JSON.parse(haslog.replace(reg, '"'));

        if (Invoicing.length < 5) {
            $("#more").remove();
        }
        for (var i = 0; i < Invoicing.length; i++) {
            addLog(Invoicing[i].LogName, Invoicing[i].LogDate, Invoicing[i].Service, Invoicing[i].Count, Invoicing[i].Name, Invoicing[i].InvoicingDate, Invoicing[i].Amount,Invoicing[i].ID,Invoicing[i].ServiceID);
        }
    }
}

//添加日志（加了个服务）
function addLog(logName, date, service, Count, name, InDate, Amount,ID,ServiceID) {
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
    h3.innerHTML = "开票说明：" + logName;
    var p_service = document.createElement("p");
    p_service.innerHTML = "服务款项：" + service;
    var vCount = document.createElement("p");
    vCount.innerHTML = "开票数量：" + Count+"张";
    var vAmount = document.createElement("p");
    vAmount.innerHTML = "开票金额：" + Changemoney(Amount)+"元";
    var vInDate = document.createElement("p");
    vInDate.innerHTML = "开票日期：" + InDate;
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = date + " by " + name;
   
    div_content.appendChild(h3);
    div_content.appendChild(p_service)
    div_content.appendChild(vCount)
    div_content.appendChild(vAmount)
    div_content.appendChild(vInDate)
    var a = document.createElement("a");
    a.className = "cd-read-more";
    a.href = "InvoicingLogModification?logName=" + logName + "&service=" + service + "&Count=" + Count + "&Amount=" + Amount + "&ID=" + ID + "&InDate=" + InDate + "&ServiceID=" + ServiceID;
    a.innerHTML = "修改"
    div_content.appendChild(a);
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}


function fillService(Accountant) {
    var services = document.getElementById("txt_services");
    for (var i = 0; i < Accountant.length; i++) {
        var service = document.createElement("option");
        service.innerHTML = Accountant[i].Service;
        service.value = Accountant[i].Service;
        services.appendChild(service);
    }
}

function serviceChanged(service) {
    for (var i = 0; i < Accountant.length; i++) {
        if (Accountant[i].Service == service) {
           
            fullServiceData(Accountant[i]);
        }
    }
}

//填充每个服务的数据
function fullServiceData(Ac) {
    
    document.getElementById("lbl_count").innerHTML = "已开票数量：" + Changemoney(Ac.SubInvoiceCount);
    document.getElementById("lbl_amount").innerHTML = "合同名称：" + Changemoney(Ac.SubInvoiceAmount);

}

function lazyLoad() {
    N = N + 5;
    $.ajax({
        type: "POST",
        url: "/Invoicing/InvoicingLogAjax?ID=" + N,
        async: true,
        cache: false,
        success: function (data) {
            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
            var Invoicing = JSON.parse(data.replace(reg, '"'));

            if (Invoicing.length < 5) {
                $("#more").remove();
            }
            if (data == "[]") {
                N -= 5;
                //show("暂无最新数据！");
                alert("暂无最新数据！");
                return false;
            }
            for (var i = 0; i < Invoicing.length; i++) {
                if (!document.getElementById("cd-timeline")) {
                    return false;
                }
                addLog(Invoicing[i].LogName, Invoicing[i].LogDate, Invoicing[i].Service, Invoicing[i].Count, Invoicing[i].Name, Invoicing[i].InvoicingDate,Invoicing[i].Amount,Invoicing[i].ID,Invoicing[i].ServiceID);
            }
        },
        error: function () {
            show("请求失败");
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