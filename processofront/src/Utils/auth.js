export const usuarioAutenticado = () => localStorage.getItem('usuario-login') !== null;

export const parseJwt = () => {
    let base64 = localStorage.getItem('user-token').split('.')[1];
    return JSON.parse(window.atob(base64));
};