
function getArray() {
    const string = document.getElementById("edit").value;
    const array = string.split(" ");
    array.sort(function (a, b){return a-b});
    return array;
}

function makeTable() {
    const array = getArray();
    const table = document.getElementById("table");
    let cnt = 0;
    for(let i=0; i < array.length/5; i++){
        const row = document.createElement('tr');
        for(let j=0; j < 5; j++){
            const cell = document.createElement('td');
            cell.innerHTML=array[cnt];
            if(array[cnt] === undefined){
                table.appendChild(row);
                return;
            }
            cnt++;
            row.appendChild(cell);
        }
        table.appendChild(row);
    }

}