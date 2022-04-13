
function getdata(valueText,type) {

    $.ajax({
        url: "/search/searchStudent",
        success: function (result) {                       
            filltable(result);
        },
        error: function (xhr, textStatus, error) {
            alert(xhr.statusText);
        },
        failure: function (response) {
            alert("failure " + response.responseText);
        },
        data: {
            "id": valueText
            ,
            "type": type
        },
        method: "GET"
    });
}

function filltable(data) {
  
    if (data != undefined) {
        let array = data.toString().split(',');
        let table = document.getElementById("TableResult");
        let text="";

        array.forEach(element => {
            let first = element.split(':')[0];
            let vaccinated = element.split(':')[1];
            if (vaccinated == "True") {
                text += `<tr class="table-success" > <td >${first}</td><td>نعم</td> </tr>`
            } else if (vaccinated == "False") {
                text += `<tr class="table-danger" > <td>${first}</td><td>لا</td> </tr>`
            }
            
        })
        if (text != "" || text != undefined) {
            table.innerHTML = text;
        }                

    }
}
   

