const imageContainer = document.getElementById("imageContainer");
const draggedImage = document.getElementById("draggedImage");
const fileImage = document.getElementById("file-image");
const downloadOriginalButton = document.getElementById("downloadOriginal");
const downloadTransparentButton = document.getElementById("downloadTransparent");

imageContainer.addEventListener("dragover", (e) => {
    e.preventDefault();
});

imageContainer.addEventListener("drop", (e) => {
    e.preventDefault();
    const file = e.dataTransfer.files[0];

    if (file && file.type.includes("image")) {
        const reader = new FileReader();

        reader.onload = () => {
            draggedImage.src = reader.result;
            imageContainer.style.border = "none";
            imageContainer.style.backgroundColor = "#ffffff";
            fileImage.style.display = "none";
        };

        reader.readAsDataURL(file);
    }
});

fileImage.addEventListener("change", (e) => {
    const file = e.target.files[0];

    if (file && file.type.includes("image")) {
        const reader = new FileReader();

        reader.onload = () => {
            draggedImage.src = reader.result;
            imageContainer.style.border = "none";
            imageContainer.style.backgroundColor = "#ffffff";
            fileImage.style.display = "none"; // Oculta o input de arquivo
        };

        reader.readAsDataURL(file);
    }
});

downloadOriginalButton.addEventListener("click", () => {
    if (!draggedImage.src) {
        alert("Por favor, carregue uma imagem antes de baixar a imagem original.");
        return;
    }

    downloadImage(draggedImage.src, "original_image.png");
});

downloadTransparentButton.addEventListener("click", () => {
    if (!draggedImage.src) {
        alert("Por favor, carregue uma imagem antes de baixar a imagem transparente.");
        return;
    }

    // Chamada da API para baixar a imagem transparente
    // Implemente a lógica para chamar sua API
    // Substitua a chamada de exemplo abaixo pela chamada da sua API
    const apiEndpoint = "https://sua-api.com/gerar-imagem-sem-fundo";
    const requestData = {
        image: draggedImage.src,
        fileName: "transparent_image.png",
    };

    // Exemplo de chamada de API com o Fetch
    fetch(apiEndpoint, {
        method: "POST",
        body: JSON.stringify(requestData),
        headers: {
            "Content-Type": "application/json",
        },
    })
        .then((response) => response.blob())
        .then((blob) => {
            const url = URL.createObjectURL(blob);
            downloadImage(url, "transparent_image.png");
        })
        .catch((error) => {
            console.error("Erro na chamada da API:", error);
        });
});

function downloadImage(url, fileName) {
    const anchor = document.createElement("a");
    anchor.setAttribute("href", url);
    anchor.setAttribute("download", fileName);
    anchor.style.display = "none";
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
}
