const Textbox = ({change, value, disabled, className=""}) => {
    return (
        <input type="text" onChange={change} value={value} className={"form-control form-control-sm " + className} 
            disabled={disabled ? "disabled" : ""} placeholder="">
        </input>
    )
}

export default Textbox;