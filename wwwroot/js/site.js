document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150;

    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'flex';
        });
    });

    const closeButtons = document.querySelectorAll('[data-close="true"]');
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal');
            if (modal) {
                modal.style.display = 'none';
            }
        });
    });

    document.querySelectorAll('.upload-box3').forEach(previewer => {
        const trigger = previewer.querySelector('#uploadTrigger');
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('img');
        const icon = trigger.querySelector('i');

        if (trigger && fileInput && imagePreview) {
            trigger.addEventListener('click', () => fileInput.click());

            fileInput.addEventListener('change', async (event) => {
                const file = event.target.files[0];
                if (file) {
                    await processImage(file, imagePreview, previewer, previewSize);
                    if (icon) icon.style.display = 'none';
                    imagePreview.style.display = 'block';
                }
            });
        }
    });

    async function loadImage(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();

            reader.onerror = () => reject(new Error("Failed to load file."));
            reader.onload = (e) => {
                const img = new Image();
                img.onerror = () => reject(new Error("Failed to load image."));
                img.onload = () => resolve(img);
                img.src = e.target.result;
            };

            reader.readAsDataURL(file);
        });
    }

    async function processImage(file, imagePreview, previewer, previewSize = 150) {
        try {
            const img = await loadImage(file);
            const canvas = document.createElement('canvas');
            canvas.width = previewSize;
            canvas.height = previewSize;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, previewSize, previewSize);
            imagePreview.src = canvas.toDataURL('image/jpeg');
            imagePreview.style.display = 'block';
            previewer.classList.add('selected');
        } catch (error) {
            console.error('Failed on image-processing:', error);
        }
    }

    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            clearErrorMessages(form);
            const formData = new FormData(form);

            try {
                const res = await fetch(form.action, {
                    method: 'POST',
                    body: formData
                });

                if (res.ok) {
                    const modal = form.closest('.modal');
                    if (modal)
                        modal.style.display = 'none';
                    window.location.reload();
                } else if (res.status === 400) {
                    const data = await res.json();
                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            const input = form.querySelector(`[name="${key}"]`);
                            if (input) {
                                input.classList.add('input-validation-error');
                            }

                            const span = form.querySelector(`[data-valmsg-for="${key}"]`);
                            if (span) {
                                span.innerText = data.errors[key].join('\n');
                                span.classList.add('field-validation-error');
                            }
                        });
                    }
                }
            } catch (err) {
                console.log('Error submitting the form:', err);
            }
        });
    });

    function clearErrorMessages(form) {
        form.querySelectorAll('[data-val="true"]').forEach(input => {
            input.classList.remove('input-validation-error');
        });

        form.querySelectorAll('[data-valmsg-for]').forEach(span => {
            span.innerText = '';
            span.classList.remove('field-validation-error');
        });
    }

    function addErrorMessage(key, errorMessage) {
        const input = document.querySelector(`[name="${key}"]`);
        const span = document.querySelector(`[data-valmsg-for="${key}"]`);
        if (input) input.classList.add('input-validation-error');
        if (span) {
            span.innerText = errorMessage;
            span.classList.add('field-validation-error');
        }
    }



    document.querySelectorAll("select").forEach(select => {
        select.addEventListener("change", function () {
            document.querySelector("[name='Date']").value =
                document.getElementById("dobYear").value + "-" +
                document.getElementById("dobMonth").value + "-" +
                document.getElementById("dobDay").value;
        });
    });



    document.querySelector("form").addEventListener("submit", function (e) {
        const status = document.querySelector("select[name='Status']").value;
        if (!status) {
            alert("Please select a status.");
            e.preventDefault();
        }
    });
});



document.querySelectorAll('.dropdown-toggle').forEach(button => {
    button.addEventListener('click', e => {
        e.stopPropagation();
        const dropdown = button.nextElementSibling;
        dropdown.classList.toggle('show');
    });
});

document.addEventListener('click', () => {
    document.querySelectorAll('.dropdown-menu.show').forEach(menu => {
        menu.classList.remove('show');
    });
});

// Click on "Edit" inside dropdown
document.querySelectorAll('.edit-project-btn').forEach(button => {
    button.addEventListener('click', function (e) {
        e.stopPropagation();

        const projectId = this.getAttribute('data-project-id');

      
        document.getElementById('projectId').value = projectId;
        document.getElementById('editTitle').value = "Sample Title";
        document.getElementById('editCompany').value = "Sample Company";
        document.getElementById('editDescription').value = "Some description";

        // Show modal
        document.getElementById('editProjectModal').style.display = 'block';
    });
});

// Close modal
document.querySelector('#editProjectModal .close').addEventListener('click', () => {
    document.getElementById('editProjectModal').style.display = 'none';
});



