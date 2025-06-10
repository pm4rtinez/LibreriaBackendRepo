using Data.Access.Entities.Comprobantes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Data.Access.Interfaces;

namespace Business.Services.Comprobantes
{
    public class ComprobanteService
    {

        public async Task<bool> GuardarDniYFirmaAsync(long comprobanteId, string dni, string firmaBase64, IUnitOfWork unitOfWork)
        {
            var comprobante = await unitOfWork.ComprobanteDevolucion
                .GetByIdAsync(comprobanteId, includeProperties: "Reserva");

            if (comprobante == null || comprobante.Reserva == null)
                return false;

            comprobante.DNI = dni;

            // Elimina encabezado como "data:image/png;base64,"
            var base64Data = Regex.Replace(firmaBase64, @"^data:image\/[a-zA-Z]+;base64,", "");
            comprobante.FirmaImagen = Convert.FromBase64String(base64Data);

            // Marca la reserva como que tiene comprobante
            comprobante.Reserva.TieneComprobante = true;

            // MARCAR LIBRO COMO DISPONIBLE
            var libro = await unitOfWork.Libros.GetByIdAsync(comprobante.Reserva.LibroId);
            if (libro != null)
            {
                libro.Disponible = true;
                unitOfWork.Libros.Update(libro);
            }

            unitOfWork.ComprobanteDevolucion.Update(comprobante);
            unitOfWork.Reserva.Update(comprobante.Reserva); // marcar reserva como modificada
            await unitOfWork.SaveChangesAsync();

            return true;
        }


        public byte[] GenerarPdfDevolucion(ComprobanteDevolucion comprobante)
        {
            using var ms = new MemoryStream();
            var doc = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter.GetInstance(doc, ms);

            doc.Open();

            // Título principal
            var titulo = new Paragraph("Comprobante de Devolución de Libro")
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
            titulo.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            doc.Add(titulo);

            // Cuerpo del texto con detalles
            var cuerpo = new Paragraph($@"
                    El usuario ha realizado la devolución de un libro con éxito.

                    Detalles del comprobante:
                    - Título del libro: {comprobante.Reserva.Libro.Titulo}
                    - DNI del usuario: {comprobante.DNI}
                    - Fecha de devolución: {DateTime.Now:dd/MM/yyyy HH:mm}
                    ")
            {
                SpacingAfter = 20f
            };
            cuerpo.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            doc.Add(cuerpo);

            // Imagen de la firma (si existe)
            if (comprobante.FirmaImagen != null && comprobante.FirmaImagen.Length > 0)
            {
                var firmaImg = iTextSharp.text.Image.GetInstance(comprobante.FirmaImagen);
                firmaImg.ScaleToFit(250f, 150f);
                firmaImg.Alignment = Element.ALIGN_CENTER;
                firmaImg.SpacingBefore = 10f;

                doc.Add(new Paragraph("Firma del usuario:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)) { Alignment = Element.ALIGN_CENTER });
                doc.Add(firmaImg);
            }

            // Pie de página
            var pie = new Paragraph("\nEste documento ha sido generado automáticamente por el sistema de gestión de biblioteca.",
                FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10))
            {
                SpacingBefore = 30f,
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(pie);

            doc.Close();
            return ms.ToArray();
        }
    }
}
