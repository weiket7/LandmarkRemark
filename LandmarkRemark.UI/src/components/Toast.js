import { Toast as BootstrapToast } from "bootstrap";
import { useEffect, useRef, useState } from "react";

const Toast = ({forwardRef}) => {
    const [message, setMessage] = useState();
    const [type, setType] = useState();
    const [toast, setToast] = useState();
    const toastRef = useRef();

    forwardRef((message, type) => showToast(message, type));

    const showToast = (message, type="success") => {
        setMessage(message);
        setType(type);
        toast.show();
    }

    useEffect(() => {
        const toast = new BootstrapToast(toastRef.current, {delay: 2000})
        setToast(toast);
    }, []);

    return (
        <div className="position-fixed top-10 start-50 p-3" style={{ zIndex: "999" }}>
            <div ref={toastRef} className={'toast text-white bg-' + type}
            role="alert" aria-live="assertive" aria-atomic="true">
            <div className="toast-body">{message}</div>
            </div>
        </div>
    )
}

export default Toast;