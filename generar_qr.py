#!/usr/bin/env python3
"""Genera QR PNG/SVG y afiche SVG editable en Illustrator para una página pública de GitHub Pages."""
import argparse
import html
import re
import sys
from pathlib import Path
from urllib.parse import urlparse
try:
    import qrcode
    from qrcode.constants import ERROR_CORRECT_H
except ImportError:
    sys.exit('Instala la dependencia: python3 -m pip install qrcode[pil]')

ROOT = Path(__file__).resolve().parent
NAVY = '#4b3026'

def poster(qr_markup, status):
    return f'''<svg xmlns="http://www.w3.org/2000/svg" width="210mm" height="297mm" viewBox="0 0 794 1123" role="img" aria-label="Afiche Bruma Café con código QR del menú">
<rect width="794" height="1123" fill="#f7f1e8"/><rect x="35" y="35" width="724" height="1053" rx="22" fill="{NAVY}"/>
<g fill="none" stroke="#fff" stroke-width="5" stroke-linejoin="round" stroke-linecap="round"><path d="M362 76H421V108C421 125 407 137 391 137C375 137 362 125 362 108V76Z"/><path d="M421 84H431C445 84 449 96 442 104C437 111 429 112 421 112"/><path d="M377 65C372 60 377 55 381 50M395 65C390 60 395 55 399 50"/><path d="M352 140H432"/></g><g fill="#fff" text-anchor="middle"><text x="397" y="230" font-family="Georgia,serif" font-size="82">BRUMA</text><text x="397" y="268" font-family="Arial,sans-serif" font-size="22" letter-spacing="8">CAFÉ</text><line x1="290" y1="301" x2="504" y2="301" stroke="#fff"/><text x="397" y="360" font-family="Georgia,serif" font-size="46">NUESTRA CARTA</text><text x="397" y="400" font-family="Arial,sans-serif" font-size="22">Escanea y descubre el menú</text></g>
<rect x="180" y="412" width="434" height="434" rx="18" fill="#fff"/>{qr_markup}
<g fill="#fff" text-anchor="middle" font-family="Arial,sans-serif"><text x="397" y="914" font-size="22">CAFÉ  ·  PAN  ·  POSTRES</text><text x="397" y="965" font-size="18">{status}</text></g></svg>'''

def placeholder():
    inner='<rect x="218" y="450" width="358" height="358" rx="12" fill="#f7f1e8"/><text x="397" y="620" text-anchor="middle" fill="#4b3026" font-family="Arial,sans-serif" font-size="24">QR pendiente</text><text x="397" y="655" text-anchor="middle" fill="#4b3026" font-family="Arial,sans-serif" font-size="16">del sitio web</text>'
    (ROOT/'design/afiche-illustrator.svg').write_text(poster(inner, 'BRUMA CAFÉ'), encoding='utf-8')

def generate(url):
    parsed=urlparse(url)
    if parsed.scheme != 'https' or not parsed.netloc:
        sys.exit('Se requiere una URL pública HTTPS completa.')
    qr=qrcode.QRCode(error_correction=ERROR_CORRECT_H, box_size=12, border=4)
    qr.add_data(url)
    qr.make(fit=True)
    matrix=qr.get_matrix()
    n=len(matrix)
    cells=''.join(f'<rect x="{x}" y="{y}" width="1" height="1"/>' for y,row in enumerate(matrix) for x,dark in enumerate(row) if dark)
    svg=f'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 {n} {n}" shape-rendering="crispEdges"><rect width="{n}" height="{n}" fill="#fff"/><g fill="#000">{cells}</g></svg>'
    (ROOT/'design/menu-qr.svg').write_text(svg, encoding='utf-8')
    qr.make_image(fill_color='black', back_color='white').save(ROOT/'design/menu-qr.png')
    qr.make_image(fill_color='black', back_color='white').save(ROOT/'Assets/Resources/menu-qr.png')
    qr.make_image(fill_color='black', back_color='white').save(ROOT/'docs/menu-qr.png')
    scale=358/n
    markup=f'<svg x="218" y="450" width="358" height="358" viewBox="0 0 {n} {n}" shape-rendering="crispEdges"><rect width="{n}" height="{n}" fill="#fff"/><g fill="#000">{cells}</g></svg>'
    (ROOT/'design/afiche-illustrator.svg').write_text(poster(markup, 'Apunta con la cámara de tu móvil'), encoding='utf-8')
    (ROOT/'Assets/Resources/menu-url.txt').write_text(url+'\n', encoding='utf-8')
    page=ROOT/'docs/index.html'
    page_html=page.read_text(encoding='utf-8')
    safe_url=html.escape(url, quote=True)
    replacement='<div id="qr-box" class="qr-box"><a href="'+safe_url+'" target="_blank" rel="noopener"><img src="menu-qr.png" alt="Código QR para abrir el menú web"></a><p>Escanea para ver nuestra carta.</p></div>'
    page_html=re.sub(r'<div id="qr-box" class="qr-box">.*?</div>', replacement, page_html, count=1, flags=re.DOTALL)
    page.write_text(page_html, encoding='utf-8')
    print('QR y afiche listos en design/; enlace guardado para Unity.')

if __name__=='__main__':
    parser=argparse.ArgumentParser()
    parser.add_argument('url', nargs='?', help='URL pública HTTPS del menú en GitHub Pages')
    args=parser.parse_args()
    generate(args.url) if args.url else placeholder()
