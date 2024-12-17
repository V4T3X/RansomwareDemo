# Füge die Windows Forms Assembly hinzu
Add-Type -AssemblyName 'System.Windows.Forms'

# Erstelle ein neues Formular-Objekt
$FormObject = [System.Windows.Forms.Form]
$RansomForm = New-Object $FormObject

# Setze die Größe des Formulars
$RansomForm.ClientSize = '700,400'

# Erstelle ein Label und füge es dem Formular hinzu
$LabelObject = [System.Windows.Forms.Label]
$Label = New-Object $LabelObject
$Label.Text = 'Some text...'
$Label.AutoSize = $true
$Label.Location = '200,150'

# Füge das Label dem Formular hinzu
$RansomForm.Controls.Add($Label)

# Zeige das Formular an
$RansomForm.ShowDialog()
