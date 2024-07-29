const deleteEntryModal = document.getElementById("deleteEntryModal");

const rowEl = document.getElementById("delete-entry-modal-row");
const atEl = document.getElementById("delete-entry-modal-at");
const entryTypeEl = document.getElementById("delete-entry-modal-entrytype");
const amountEl = document.getElementById("delete-entry-modal-amount");
const descriptionEl = document.getElementById("delete-entry-modal-description");
const deleteBtnEl = document.getElementById("delete-entry-modal-delete-btn");

const deleteEntry = async (id) => {
    axios.delete(`/money-tracker/${id}`)
        .then(response => {
            window.location.href = window.location.href;
        })
        .catch(error => {
            console.error('There was an error!', error);
        });
}

const showDeleteEntryModal = async (rowClass, id, atFormatted, entryTypeName, amountFormatted, entryDescription) => {
    
    atEl.innerText = atFormatted;
    entryTypeEl.innerText = entryTypeName;
    amountEl.innerText = amountFormatted;
    descriptionEl.innerText = entryDescription;
    rowEl.classList.add(rowClass)
    $(deleteEntryModal).modal('show');

    deleteBtnEl.addEventListener('click', async () => {
        await deleteEntry(id);
    })
}

$(deleteEntryModal).on('hidden.bs.modal', () => {
    atEl.innerText = "";
    entryTypeEl.innerText = "";
    amountEl.innerText = "";
    descriptionEl.innerText ="";
    rowEl.className = "";
})

//document.getElementById('NewEntry_Amount').addEventListener('blur', function () {
//    // Get the current value
//    console.log('triggered')
//    let value = parseFloat(this.value);

//    // Check if the value is a valid number
//    if (!isNaN(value)) {
//        // Format the value to 2 decimal places
//        this.value = value.toFixed(2);
//    }
//});

//document.getElementById('yourForm').addEventListener('submit', function (e) {
//    e.preventDefault(); // Prevent the default form submit

//    // Serialize form data manually or using FormData
//    var formData = new FormData(this);

//    // Make POST request using Axios
//    axios.post(createEntryFormActionUrl, formData)
//        .then(function (response) {
//            var data = response.data;
//            if (data.isValid) {
//                $('#createEntryModal').modal('hide'); // Close the modal on success
//                // Optionally update UI here, e.g., refresh a list
//            } else {
//                // Update form with validation errors
//                $('#nameError').text(data.errors.Name); // Update Name field error message
//                $('#emailError').text(data.errors.Email); // Update Email field error message
//            }
//        })
//        .catch(function (error) {
//            console.error('Error submitting form', error); // Handle errors if needed
//        });
//});