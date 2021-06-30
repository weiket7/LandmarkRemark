const Button = ({children, click, colour="primary", className="", disabled}) => {
    return (
        <button type="button" className={"btn btn-sm btn-" + colour + " " + className}
            onClick={click} disabled={disabled ? "disabled" : ""}>
            <span className="indicator-label">
                { children }
            </span>
        </button>
    )
}

export default Button;