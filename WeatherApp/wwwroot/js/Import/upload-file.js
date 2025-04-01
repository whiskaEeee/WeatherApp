const dropZone = document.getElementById('dropZone')
const formFile = document.getElementById('formFile')
const errorMessage = document.getElementById('errorMessage')
const fileListContainer = document.getElementById('fileList')
const fileList = fileListContainer.querySelector('ul')

const allowedTypes = ['application/vnd.ms-excel', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet']
const maxSize = 10 * 1024 * 1024

let selectedFiles = []

function validateFile(file) {
    if (!allowedTypes.includes(file.type) && !file.name.match(/\.(xls|xlsx)$/)) {
        throw new Error('Invalid file type. Please upload Excel files (.xls, .xlsx).')
    }
    if (file.size > maxSize) {
        throw new Error(`File "${file.name}" is too large. Maximum size is 10MB.`)
    }
}

function showError(message) {
    errorMessage.textContent = message
    errorMessage.classList.remove('d-none')
}

function hideError() {
    errorMessage.classList.add('d-none')
}

function updateFileInput() {
    const dataTransfer = new DataTransfer()
    selectedFiles.forEach(file => dataTransfer.items.add(file))
    formFile.files = dataTransfer.files
}

function updateFileList() {
    fileList.innerHTML = ''
    if (selectedFiles.length === 0) {
        fileListContainer.classList.add('d-none')
        return
    }
    fileListContainer.classList.remove('d-none')

    selectedFiles.forEach((file, index) => {
        const listItem = document.createElement('li')
        listItem.classList.add('list-group-item', 'd-flex', 'justify-content-between', 'align-items-center')
        listItem.textContent = file.name

        const removeBtn = document.createElement('button')
        removeBtn.classList.add('btn', 'btn-sm', 'btn-danger')
        removeBtn.textContent = 'Remove'
        removeBtn.onclick = () => {
            selectedFiles.splice(index, 1)
            updateFileList()
            updateFileInput()
        }

        listItem.appendChild(removeBtn)
        fileList.appendChild(listItem)
    })
}

dropZone.addEventListener('click', () => formFile.click())

dropZone.addEventListener('dragover', (e) => {
    e.preventDefault()
    dropZone.classList.add('bg-light')
})

dropZone.addEventListener('dragleave', () => {
    dropZone.classList.remove('bg-light')
})

dropZone.addEventListener('drop', (e) => {
    e.preventDefault()
    dropZone.classList.remove('bg-light')
    hideError()

    try {
        const files = Array.from(e.dataTransfer.files)
        files.forEach(validateFile)
        selectedFiles = [...selectedFiles, ...files]
        updateFileList()
        updateFileInput()
    } catch (error) {
        showError(error.message)
    }
})

formFile.addEventListener('change', () => {
    hideError()
    try {
        if (formFile.files.length > 0) {
            const files = Array.from(formFile.files)
            files.forEach(validateFile)
            selectedFiles = [...selectedFiles, ...files]
            updateFileList()
            updateFileInput()
        }
    } catch (error) {
        showError(error.message)
        formFile.value = ''
    }
})