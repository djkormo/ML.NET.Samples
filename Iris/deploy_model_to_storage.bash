RND=$RANDOM

ML_LOCATION=northeurope
ML_GROUP=rg-ml-net
ML_STACCOUNT=staccmlnet$RND
ML_FUNCAPP=mlnetfuncapp$RND

# domyslna nazwa grupy 
az configure --defaults group=$ML_GROUP

# domyslna lokalizacja rejestru z obrazami 
az configure --defaults location=$ML_LOCATION

# odswiezamy zawartosc repozytorium w ramach galęzi master 
git checkout master

# pobieramy zawartość repozytorium 
git pull

# weryfikujemy niespójności zawartości lokalnego i zdalnego repozytorium 
git status



az group create --name $ML_GROUP --location $ML_LOCATION

az storage account create --name $ML_STACCOUNT --location $ML_LOCATION --resource-group $ML_GROUP --sku Standard_LRS

az functionapp create --name $ML_FUNCAPP --storage-account $ML_STACCOUNT --consumption-plan-location $ML_LOCATION --resource-group $ML_GROUP

az functionapp config appsettings set --name $ML_FUNCAPP --resource-group $ML_GROUP --settings FUNCTIONS_EXTENSION_VERSION=beta

az storage account keys list --account-name $ML_STACCOUNT --resource-group $ML_GROUP



ACC_KEY=$(az storage account keys list --resource-group $ML_GROUP --account-name $ML_STACCOUNT --query "[0].value" | tr -d '"')

az storage container create --name models --account-key $ACC_KEY --account-name $ML_STACCOUNT



az storage file upload --account-name $ML_STACCOUNT \
    --account-key $ACC_KEY \
	--share-name "models" \
	--source "MLModel.zip" \
    --path "MLModel,zip"
