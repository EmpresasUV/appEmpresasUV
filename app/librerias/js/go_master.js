/**
 * @autor Rixio Danilo Iguarán Chourio.
 * @cédula-profesional 12196442
 * @correo-eletrónico atencion.clientes@obringolfo.com
 * @denominacion OBRAS INFORMÁTICAS DEL GOLFO
 * @empresa OBRINGOLFO S.A.S. DE C.V.
 * @proyecto  goPAC.
 * @copyright 2021
 */


    /** ****************************************************** **/
    function goRedirect(controlador, accion){
        window.location.replace("/index.php?goExec=" + controlador + "&goAcc=" + accion);
    }
    /** ****************************************************** **/
    function url(controlador, accion){
        var urlString = "index.php?goExec="+controlador+"&goAcc="+accion;
        return urlString;
    }
    /** ****************************************************** **/
    function goURL(www_url){
        window.location.replace(www_url);
    }
    /** ****************************************************** **/
    function goTasaW(tasa){
        var MyV = $(window).width();
        return ((MyV * tasa)/100);
    }
    /** ****************************************************** **/
    function goTasaH(tasa){
        var MyH = $(window).height();
        return ((MyH * tasa)/100);
    }
    /** ****************************************************** **/
    function statusMSG(msg){
        $("#mensaje_escritorio").html(msg);
    }
    /** ****************************************************** **/
    function statusMSG_clear(){
        $("#mensaje_escritorio").html('');
    }
    /** ****************************************************** **/
    function show_progress(msg = ''){
        var bprogress = $.messager.progress({
            height: 80,
            cls: 'page-center',
            msg: '<div class="text-muted mb-3">'+msg+'</div><div class="progress progress-sm"><div class="progress-bar progress-bar-indeterminate"></div></div>'
        });
        $.messager.progress('bar').hide();
        bprogress.dialog('resize');
    }
    /** ****************************************************** **/
    function status_getFecha() {
        const d = new Date();
        var options = { weekday: "long", year: "numeric", month: "long", day: "numeric", hour: "numeric",  minute: "numeric", hour12: "false" };
        let div_fecha = document.getElementById("fecha_escritorio");
        if(div_fecha != null){
            div_fecha.innerHTML = d.toLocaleString("es-MX", options);
        }else{
            clearInterval(window.statusDATE);
        }
    }    
    /** ****************************************************** **/
    $.extend($.fn.combobox.methods, {
        appendItem: function(jq, item){
            return jq.each(function(){
                var state = $.data(this, 'combobox');
                var opts = state.options;
                var items = $(this).combobox('getData');
                items.push(item);
                $(this).combobox('panel').append(
                    '<div id="' + state.itemIdPrefix+'_'+(items.length-1) + '"  class="combobox-item">' +
                    (opts.formatter ? opts.formatter.call(this, item) : item[opts.textField]) +
                    '</div>'
                )
            })
        }
    })
    /** ****************************************************** **/
    $.extend($.fn.textbox.defaults.rules, {
        satRFC: {
            validator: function(value, param){
                return rfcValido(value, param[0]);
            },
            message: 'Ingrese un RFC válido.'
        },
        renapoCURP: {
            validator: function(value, param){
                return curpValida(value);
            },
            message: 'Ingrese una CURP válida.'
        }
    });
    /** ****************************************************** **/
    /** ****************************************************** **/
    /**
     * Función para validar un RFC 
     * Devuelve el RFC sin espacios ni guiones si es correcto
     * Devuelve false si es inválido
     * (debe estar en mayúsculas, guiones y espacios intermedios opcionales)
     * ****************************************************** **/
    function rfcValido(rfc, aceptarGenerico = true) {
        const re       = /^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/;
        var   validado = rfc.match(re);

        if (!validado)  //Coincide con el formato general del regex?
            return false;

        //Separar el dígito verificador del resto del RFC
        const digitoVerificador = validado.pop(),
            rfcSinDigito      = validado.slice(1).join(''),
            len               = rfcSinDigito.length,

        //Obtener el digito esperado
            diccionario       = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
            indice            = len + 1;
        var   suma,
            digitoEsperado;

        if (len == 12) suma = 0
        else suma = 481; //Ajuste para persona moral

        for(var i=0; i<len; i++)
            suma += diccionario.indexOf(rfcSinDigito.charAt(i)) * (indice - i);
        digitoEsperado = 11 - suma % 11;
        if (digitoEsperado == 11) digitoEsperado = 0;
        else if (digitoEsperado == 10) digitoEsperado = "A";

        //El dígito verificador coincide con el esperado?
        // o es un RFC Genérico (ventas a público general)?
        if ((digitoVerificador != digitoEsperado)
        && (!aceptarGenerico || rfcSinDigito + digitoVerificador != "XAXX010101000"))
            return false;
        else if (!aceptarGenerico && rfcSinDigito + digitoVerificador == "XEXX010101000")
            return false;
        return rfcSinDigito + digitoVerificador;
    }    

    /** 
     * Función para validar la CURP 
     * Devuelve la CURP sin espacios ni guiones si es correcto
     * Devuelve false si es inválido
     * (debe estar en mayúsculas, guiones y espacios intermedios opcionales)
     * ****************************************************** **/
    function curpValida(curp) {
        var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
            validado = curp.match(re);
        
        if (!validado)  //Coincide con el formato general?
            return false;
        
        //Validar que coincida el dígito verificador
        function digitoVerificador(curp17) {
            //Fuente https://consultas.curp.gob.mx/CurpSP/
            var diccionario  = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
                lngSuma      = 0.0,
                lngDigito    = 0.0;
            for(var i=0; i<17; i++)
                lngSuma= lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
            lngDigito = 10 - lngSuma % 10;
            if(lngDigito == 10)
                return 0;
            return lngDigito;
        }
        if (validado[2] != digitoVerificador(validado[1])) 
            return false;
            
        return true; //Validado
    }
     /** ****************************************************** **/
    function check_privilegios(_modulo, _funcion){
        //VERIFICANDO PERMISOS DEL USUARIO
        var xData = { modulo: _modulo, funcion: _funcion };
        return $.ajax({
            type: 'POST',
            url: '/index.php?goExec=Usuarios&goAcc=check_privilegio',
            data: xData,
            cache: false,            
            success:function(respuesta){
                //console.log('respuesta: '+respuesta);   
            }
        });
    }     
/** ********************************************************************************** **/
    function screen_TasaW(tasa, condicion = null, valor = null){
        var MyV = $(window).width();
        if((condicion != null) && (valor != null)){
            switch (condicion) {
                case "=":
                    if(MyV == valor){
                        return ((MyV * tasa)/100);
                    }else{
                        return valor;
                    }
                break
                case "<":
                    if(MyV < valor){
                        return ((MyV * tasa)/100);
                    }else{
                        return valor;
                    }
                break
                case ">":
                    if(MyV > valor){
                        return ((MyV * tasa)/100);
                    }else{
                        return valor;
                    }
                break
            }
        }else{
            return ((MyV * tasa)/100);
        }
    }
/** ********************************************************************************** **/
    function screen_TasaH(tasa, condicion = null, valor = null){
        var MyH = $(window).height();
        if((condicion != null) && (valor != null)){
            switch (condicion) {
                case "=":
                    if(MyH == valor){
                        return ((MyH * tasa)/100);
                    }else{
                        return valor;
                    }
                break
                case "<":
                    if(MyH < valor){
                        return ((MyH * tasa)/100);
                    }else{
                        return valor;
                    }
                break
                case ">":
                    if(MyH > valor){
                        return ((MyH * tasa)/100);
                    }else{
                        return valor;
                    }
                break
            }
        }else{
            return ((MyH * tasa)/100);
        }
    }
/** ********************************************************************************** **/
    function GeneratePassword(longitd = 14){
        var chars = "0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var password = "";
        for (var i = 0; i <= longitd; i++) {
            var randomNumber = Math.floor(Math.random() * chars.length);
            password += chars.substring(randomNumber, randomNumber +1);
        }
        return password;
    }
/** ********************************************************************************** **/
function chk_valid(intput){
    var xRegex = '';
    //console.log(intput.id +" = "+ intput.required);
    if((intput.required == true) || (intput.required == "required")){
        switch (intput.type) {
            case 'text':
                if(intput.getAttribute("pattern")){
                    isValid = intput.validity.patternMismatch;
                }else if(intput.getAttribute("patron")){
                    switch (intput.getAttribute("patron")) {
                        case "numeros":
                            xRegex = /^[0-9]+$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'nombres':
                            xRegex = /^[A-Za-zÁÉÍÓÚÑáéíóúñ ]{2,}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'apellidos':
                            xRegex = /^[A-Za-zÁÉÍÓÚÑáéíóúñ]{2,}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'string-guiones-espacios':
                            xRegex = /^[A-Za-zÁÉÍÓÚÑáéíóúñ -_]{2,}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'string-numeros-guiones-espacios':
                            xRegex = /^[A-Za-zÁÉÍÓÚÑáéíóúñ -_0-9]{2,}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'rfc':
                            isValid = rfcValido(intput.value.trim().toUpperCase());
                        break
                        case 'cp':
                            xRegex = /^[0-9]{4,6}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'telefono':
                            xRegex = /^[0-9]{10,10}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'num_calle':
                            xRegex = /^[a-zA-Z0-9]{1,4}$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case 'gps':
                            xRegex = /^-?([0-9]{1,2}|1[0-7][0-9]|180)(\.[0-9]{4,16})$/;
                            isValid = xRegex.test(intput.value);
                        break
                        case "url":
                            xRegex = /^(((ftp|http|https):\/\/)|(\/)|(..\/))(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?$/;
                            isValid = xRegex.test(intput.value);
                        break
                    }
                }

            break
            case 'password':
                //isValid = intput.validity.patternMismatch; (^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$)
                xRegex = /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$/;
                if(xRegex.test(intput.value)){ //debemos de colocar el borde del boton mostrar clave
                    isValid = true;
                    $(intput).next().css("border-color", "#2fb344");
                }else{
                    isValid = false;
                    $(intput).next().css("border-color", "#d63939");
                }
            break
            case 'email':
                xRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                if (xRegex.test(intput.value)){
                    isValid = true;
                }else{
                    isValid = false;
                }
            break
            case 'hidden':
                isValid = true;
            break
            case 'select-one':
                isValid = true;
            break
            case 'file':
                console.log(intput.files.length);
                if(intput.files.length){
                    isValid = true;
                }else{
                    isValid = false;
                }
            break
        }
    }else{
        isValid = true;
    }
    if(isValid){
        intput.classList.remove("is-invalid");
        intput.classList.add("is-valid");
    }else{
        intput.classList.remove("is-valid");
        intput.classList.add("is-invalid");
    }
    return isValid;
}
/** ********************************************************************************** **/
    function read_json(data){
        //console.log(data.constructor.name);
        if(data.constructor.name == 'String'){
            if(data.substring(0, 1) == '{'){ //tenemos un posible objeto
                var rJSON = JSON.parse(data);
                if(rJSON.code){
                    var form = '';
                    $.each(rJSON, function(key, value) {
                        //console.log(key +" = "+ value);
                        form += '<input type="hidden" name="'+key+'" value="'+value+'">';
                    });
                    $('<form action="/index.php?goExec=Excepciones&goAcc=show_error" method="POST">' + form + '</form>').appendTo($(document.body)).submit();
                }else{
                    return rJSON
                }
            }else{
                document.write(data);
            }
        }else{
            return data;
        }
    }
/** ********************************************************************************** **/
function show_password(inpass, cmd){
    //console.log($("#"+cmd.id).html());
    let hiddenICO   = '<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-eye-off" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M3 3l18 18"></path><path d="M10.584 10.587a2 2 0 0 0 2.828 2.83"></path><path d="M9.363 5.365a9.466 9.466 0 0 1 2.637 -.365c4 0 7.333 2.333 10 7c-.778 1.361 -1.612 2.524 -2.503 3.488m-2.14 1.861c-1.631 1.1 -3.415 1.651 -5.357 1.651c-4 0 -7.333 -2.333 -10 -7c1.369 -2.395 2.913 -4.175 4.632 -5.341"></path></svg>';
    let showICO     = '<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><circle cx="12" cy="12" r="2" /><path d="M22 12c-2.667 4.667 -6 7 -10 7s-7.333 -2.333 -10 -7c2.667 -4.667 6 -7 10 -7s7.333 2.333 10 7" /></svg>';
    if(document.getElementById(inpass).attributes["type"].value == "password"){
        document.getElementById(inpass).attributes["type"].value = "text";
        $("#"+cmd.id).html(hiddenICO);
    }else{
        document.getElementById(inpass).attributes["type"].value = "password";
        $("#"+cmd.id).html(showICO);
    }

}
/** ********************************************************************************** **/
function show_asistente(tipo, texto, alto = 400, ancho = 300){
    $.messager.show({
        cls:'messager-asistente',
        timeout: 9000,
        showType: 'show',
        width: ancho,
        height: alto,
        border:'thin',
        msg: '<div class="text-center mb-4"><span class="asistente-robot" style="background-image: url(/app/librerias/img/robotips/asistente_'+tipo+'.gif);"></span></div>' + texto,
        style:{
            left: document.body.clientWidth - (ancho + 15),
            right:'',
            top:'',
            bottom: (-document.body.scrollTop - document.documentElement.scrollTop) + 15
        }
    });
}
/** ********************************************************************************** **/
function ExisteError(respuesta){
    var jsonRCP = read_json(respuesta);
    var winTitulo = "";
    var winMensaje = "";
    var is_error = false;
    switch (respuesta.Code) {
        case 400:
            winTitulo = "Solicitud inválida";
            winMensaje = "El Sdk de ContPAQi no pudo interpretar la solicitud, la sintaxis del comando enviado es inválida.";
            is_error = true;
        break
        case 1999:
            winTitulo = "Problema con éste producto";
            winMensaje = "La existencia del almacen no cubre la cantidad solicitada para el producto.";
            is_error = true;
        break
        case 1998:
            winTitulo = "Problema con éste producto";
            winMensaje = "Producto sin existencia en el almacén";
            is_error = true;
        break
        case 1997:
            winTitulo = "Problema con éste producto";
            winMensaje = "Éste producto está inactivo y no puede ser vendido.";
            is_error = true;
        break
        case 1996:
            winTitulo = "Problema con éste producto";
            winMensaje = "El producto solicitado no está registrado en el sistema.";
            is_error = true;
        break
        case 1995:
            winTitulo = "Problema con éste producto";
            winMensaje = "No hay un precio asignado para los boletos";
            is_error = true;
        break



        default:
            is_error = false;
    }
    if(is_error){
        $("#cmdERROR")[0].click();
        $("#titulo").html(winTitulo);
        $("#mensaje").html(winMensaje);
    }
    return is_error;
}
/** ********************************************************************************** **/
/** ********************************************************************************** **/
/** ********************************************************************************** **/
