import requests
from bs4 import BeautifulSoup
from urllib.parse import urljoin
import json

def scrape_website(url, max_depth=2):
    visited = set()
    link_tree = {}
    
    def scrape_page(current_url, depth=0):
        if depth > max_depth or current_url in visited:
            return
        
        visited.add(current_url)
        try:
            response = requests.get(current_url)
            soup = BeautifulSoup(response.text, 'html.parser')
            
            links = []
            for link in soup.find_all('a', href=True):
                href = link['href']
                absolute_url = urljoin(current_url, href)
                
                if absolute_url.startswith(url) and absolute_url not in visited:
                    links.append(absolute_url)
                    scrape_page(absolute_url, depth + 1)
            
            link_tree[current_url] = links
        except Exception as e:
            print(f"Error scraping {current_url}: {str(e)}")
    
    scrape_page(url)
    return link_tree

def save_to_json(link_tree, filename='link_tree.json'):
    with open(filename, 'w', encoding='utf-8') as f:
        json.dump(link_tree, f, ensure_ascii=False, indent=2)

if __name__ == "__main__":
    target_url = "https://www.yukes.co.jp/"
    link_tree = scrape_website(target_url)
    save_to_json(link_tree) 