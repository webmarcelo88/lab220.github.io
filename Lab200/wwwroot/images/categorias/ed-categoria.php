<?php
include_once 'inc_cab_logged.php';
$menu_selecionado = array('Categorias', '');
$custom_folder = "cliente";
$custom_header_files = "categoria";
$custom_footer_files = "ed-categoria";
include_once 'app/includes/inc_shared_header.php';
include_once 'categoria_db.php';
$categoria = get_categoria($_GET["ed"]);

?>
  <style>
 
 .container {
  max-width: 1260px;
}
.form-control, .dataTables_filter input {
    height: 38px;
    border-radius: 6px;
}

.lh-condensed { line-height: 1.25; }

label {
    font-weight: 600;
}

form .form-control::-webkit-input-placeholder { 
  color: #dcdcdc;
}

form .form-control::-moz-placeholder {
    color: #dcdcdc;
}
form .form-control:-ms-input-placeholder {
    color: #dcdcdc;
}
form .form-control:placeholder {
    color: #dcdcdc;
}
.input-group-text {
    padding-top: 0;
    padding-bottom: 0;
    align-items: center;
    border-radius: 8px;
}
form .form-control::-ms-expand {
    display: none;
}

.switch {
}

.switch input[type=checkbox] {
    display: none;
}

.switch input[type=checkbox] + label {
    position:relative;
    min-width:calc(calc(2.375rem * .8) * 2);
    border-radius:calc(2.375rem * .8);
    height:calc(2.375rem * .8);
    line-height:calc(2.375rem * .8);
    display:inline-block;
    cursor:pointer;
    outline:none;
    user-select:none;
    vertical-align:middle;
    text-indent:calc(calc(calc(2.375rem * .8) * 2) + .5rem);
}
.switch input[type=checkbox] + label::before,
.switch input[type=checkbox] + label::after {
    content:'';
    position:absolute;
    top:0;
    left:0;
    width:calc(calc(2.375rem * .8) * 2);
    bottom:0;
    display:block;
}
.switch input[type=checkbox] + label::before {
right:0;
background-color:#dee2e6;
border-radius:calc(2.375rem * .8);
transition:.2s all;
}
.switch input[type=checkbox] + label::after {
    top:2px;
    left:2px;
    width:calc(calc(2.375rem * .8) - calc(2px * 2));
    height:calc(calc(2.375rem * .8) - calc(2px * 2));
    border-radius:50%;
    background-color:#fff;
    transition: all 0.3s ease-in 0s;;
}
.switch input[type=checkbox]:checked + label::before {
    background-color:#08d;
}
.switch input[type=checkbox]:checked + label::after {
    margin-left:calc(2.375rem * .8);
}
.switch input[type=checkbox]:focus + label::before {
    outline:none;
    box-shadow:0 0 0 .2rem rgba(0,136,221,.25);
}
.switch input[type=checkbox]:disabled + label {
    color:#868e96;
    cursor:not-allowed;
}
.switch input[type=checkbox]:disabled + label::before {
    background-color:#e9ecef;
}

.switch.switch-xs {
    font-size:.8rem;
}
.switch.switch-xs input[type=checkbox] + label {
    min-width:calc(calc(1.5375rem * .8) * 2);
    height:calc(1.5375rem * .8);
    line-height:calc(1.5375rem * .8);
    text-indent:calc(calc(calc(1.5375rem * .8) * 2) + .5rem);
}
.switch.switch-xs input[type=checkbox] + label::before {
    width:calc(calc(1.5375rem * .8) * 2);
}
.switch.switch-xs input[type=checkbox] + label::after {
    width:calc(calc(1.5375rem * .8) - calc(2px * 2));
    height:calc(calc(1.5375rem * .8) - calc(2px * 2));
}
.switch.switch-xs input[type=checkbox]:checked + label::after {
    margin-left:calc(1.5375rem * .8);
}

/* Small */
.switch.switch-sm {
    font-size:.875rem;
}
.switch.switch-sm input[type=checkbox] + label {
    min-width:calc(calc(1.9375rem * .8) * 2);
    height:calc(1.9375rem * .8);
    line-height:calc(1.9375rem * .8);
    text-indent:calc(calc(calc(1.9375rem * .8) * 2) + .5rem);
}
.switch.switch-sm input[type=checkbox] + label::before {
    width:calc(calc(1.9375rem * .8) * 2);
}
.switch.switch-sm input[type=checkbox] + label::after {
    width:calc(calc(1.9375rem * .8) - calc(2px * 2));
    height:calc(calc(1.9375rem * .8) - calc(2px * 2));
}
.switch.switch-sm input[type=checkbox]:checked + label::after {
    margin-left:calc(1.9375rem * .8);
}

/* Large */
.switch.switch-lg {
    font-size:1.25rem;
}
.switch.switch-lg input[type=checkbox] + label {
    min-width:calc(calc(3rem * .8) * 2);
    height:calc(3rem * .8);
    line-height:calc(3rem * .8);
    text-indent:calc(calc(calc(3rem * .8) * 2) + .5rem);
}
.switch.switch-lg input[type=checkbox] + label::before {
    width:calc(calc(3rem * .8) * 2);
}
.switch.switch-lg input[type=checkbox] + label::after {
    width:calc(calc(3rem * .8) - calc(2px * 2));
    height:calc(calc(3rem * .8) - calc(2px * 2));
}
.switch.switch-lg input[type=checkbox]:checked + label::after {
    margin-left:calc(3rem * .8);
}

.switch + .switch {
    margin-left:1rem;
}
</style>
<div class="az-content-body" >
    <div class="row"></div>
        <div class="col-sm-12 col-md-12 col-xl-12">
            <div class="card  ht-md-100p">
                <div class="card-header">
                     <h5 class="card-title">EDIÇÃO DE CATEGORIA</h5>
                </div>
                <div class="card-body">
                <div class="alert icon-alert with-arrow alert-success form-alter" role="alert" style="display:none;">
                    <i class="fa fa-fw fa-check-circle"></i>
                    <strong> Categoria</strong> editada com sucesso.
                </div>
                <div class="alert icon-alert with-arrow alert-danger form-alter" role="alert" style="display:none;">
                    <i class="fa fa-fw fa-times-circle"></i>
                    <strong> Atenção </strong> não foi possível salvar os dados.
                </div>                
 <form id="edit-categoria-form" name="edit-categoria-form" method="post" enctype="multipart/form-data" >
 <input type="hidden" id="form_name" name="form_name" value="edit-categoria"/>
 <input type="hidden" id="edit_id" name="edit_id" value="<?php echo $categoria['id_categoria'];?>" />
    <div class="row">

        <div class="col-md-4 order-md-1 order-sm-2 order-xs-2">
              <h4 class="mb-3 titulos">Dados da Categoria</h4>


                <div class="mb-3">
                    <label for="nome">Nome *</label>
                    <input type="text" class="form-control" id="nome" name="nome" value="<?php echo $categoria['nome'];?>" autocomplete="off"  required>
                    <div class="invalid-feedback">Este campo é obrigatório.</div>
                </div>
                <div class="mb-3">
                    <label for="ordem">Ordem *</label>
                    <input type="text" class="form-control" id="ordem" name="ordem"  value="<?php echo $categoria['ordem'];?>" autocomplete="off"  required>
                    <div class="invalid-feedback">Este campo é obrigatório.</div>
                </div>
                <div class="mb-3"> 
                <label for="preview">Imagem: </label><br/>
               
                    
                    <img src="<?php echo $categoria['imagem'];?>" id="preview" alt="" width="260" style="border:1px solid #acacac" class="img-fluid upimage order-1 order-lg-2 mb-1"><br/>
                    <span id="select_image" style="cursor:pointer" class="btn-sm btn-info">Selecionar Imagem</span>
                    <input type='file' name="imgInput" id="imgInput" class="form-control-sm" style="display:none;" />
                </div>

        </div>   
        
          <div class="col-md-4 order-md-3 order-sm-1 order-xs-1 mg-b-10">
               <div class="ht-100p" style="border-left: 1px solid #cccccc; background-color: #fefefe;padding: 20px;">
               <div class="alert alert-info" style="border-left: 3px solid #adc8d8; background-color:#e9f2f7">    <h6>Atenção</h6>
                   Campos marcados com * são de preenchimento obrigatório
               </div>    </div>        
          </div>

    
</form>
</div>
<div class="row">
    <div class="col-md-12">
         <hr>
         <nav class="nav az-nav-line">
             <button class="btn btn-primary btn-lg btn-form-action btn-submit" id="btn_salvar" type="button">Salvar</button>
             <button id="btn_voltar" class="btn btn-default btn-lg btn-form-action btn-reset mg-l-10" type="button">Voltar</button>
             <span id="img-loading-submit" style="display:none"><img src="img/loading.gif" style="margin-left:10px;width:38px;height:38px;"/></span>
</nav>
        
    </div>
</div>
</div>
</div>
</div>
</div><!-- az-content-body -->

<?php
include_once('app/includes/inc_shared_footer.php');