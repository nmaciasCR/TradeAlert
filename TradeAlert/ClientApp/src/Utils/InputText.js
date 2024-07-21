export function OnlyInteger(text) {
    return (text === '' || /^[0-9]+$/.test(text));
}