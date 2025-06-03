document.addEventListener("DOMContentLoaded", () => {
    const projects = document.querySelectorAll(".project");

    projects.forEach(project => {
        const deadlineStr = project.dataset.deadline;
        if (!deadlineStr) return;

        const deadline = new Date(deadlineStr);
        const now = new Date();
        const diffTime = deadline - now;
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        const tag = project.querySelector('.tag');
        if (!tag) return;

        let text = '';

        // همیشه کلاس gray باشه
        tag.classList.remove('red', 'yellow', 'blue'); // اگه قبلاً چیزی مونده بود
        tag.classList.add('gray');

        if (diffDays > 7) {
            const weeks = Math.ceil(diffDays / 7);
            text = `${weeks} week${weeks > 1 ? 's' : ''} left`;
        } else if (diffDays > 0) {
            text = `${diffDays} day${diffDays > 1 ? 's' : ''} left`;
        } else {
            text = `Deadline passed`;
        }

        tag.innerText = text;
    });
});