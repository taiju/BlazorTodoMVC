window.focusEditField = el => {
    // TOO DIRTY!!
    setTimeout(() => {
        el.focus();
        el.setSelectionRange(el.value.length, el.value.length);
    }, 0);
}