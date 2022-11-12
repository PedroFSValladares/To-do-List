$(".delete").click((e) => {
    e.preventDefault()
    if (confirm("deseja realmente apagar está tarefa?")) {
        window.location = $(".delete").attr("href")
    }
})

$("input[name='tipoPesquisa'").change((e) => {
    let input
    switch (e.target.value) {
        case "ObterPorTitulo":
            input = document.createElement("input")
            input.type = "text"
            input.name = "parametro"
            break;
        case "ObterPorData":
            input = document.createElement("input")
            input.type = "date"
            input.name = "parametro"
            break;
        case "ObterPorStatus":
            input = document.createElement("select")
            input.name = "parametro"
            let op1 = document.createElement("option")
            op1.text = "Pendente"
            let op2 = document.createElement("option")
            op2.text = "Finalizada"
            input.appendChild(op1)
            input.appendChild(op2)
    }
    console.log(e.target.value)
    $(".input").html(input)
})
